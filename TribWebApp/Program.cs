using TribWebApp.Components;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

builder.Configuration.AddJsonFile("./TribWebApp/appsettings.json", optional: false, reloadOnChange: true);


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

// Retrieve configuration values and log them
var clientSecret = builder.Configuration["AzureAd:ClientSecret"];
var clientId = builder.Configuration["AzureAd:ClientId"];
var tenantId = builder.Configuration["AzureAd:TenantId"];

Console.WriteLine($"Client Secret: {clientSecret}");
Console.WriteLine($"Client ID: {clientId}");
Console.WriteLine($"Tenant ID: {tenantId}");

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
app.MapGet("/api/secure-data", () => "Protected data!");
    // .RequireAuthorization("ApiAccess");


app.Run();
