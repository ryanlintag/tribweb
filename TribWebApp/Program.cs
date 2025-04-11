using TribWebApp.Components;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Retrieve configuration values and log them
var clientSecret = builder.Configuration["AzureAd:AZURE_AD_CLIENT_SECRET"];
var clientId = builder.Configuration["AzureAd:AZURE_AD_CLIENT_ID"];
var tenantId = builder.Configuration["AzureAd:AZURE_AD_TENANT_ID"];

var azureAd = new AzureAd();
builder.Configuration.GetSection(AzureAd.SectionName).Bind(azureAd);
azureAd.ClientId = clientId;
azureAd.ClientSecret = clientSecret;
azureAd.TenantId = tenantId;

builder.Services.AddSingleton(azureAd);

// // Add services
// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
//     .EnableTokenAcquisitionToCallDownstreamApi()
//     .AddInMemoryTokenCaches();

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("ApiAccess", policy =>
//         policy.RequireClaim("scope", "access_as_user"));
// });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// API Endpoints
app.MapGet("/api/secure-data", (AzureAd azure) => azure.ClientId);
    // .RequireAuthorization("ApiAccess");


app.Run();
