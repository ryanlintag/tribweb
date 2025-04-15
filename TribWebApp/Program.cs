using Application;
using TribWebApp.Components;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

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

builder.Services.AddApplication();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();

 builder.Services.AddHttpClient("tribClient", config => {
        config.BaseAddress = new Uri("https://localhost:5001/");
        config.DefaultRequestHeaders.Accept.Clear();
        config.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
 });

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
app.MapGet("/api/secure-data", (IAzureConfigService azureService) => 
{
    var azure = azureService.GetConfig();
    return azure.ClientId;
});
    // .RequireAuthorization("ApiAccess");


app.Run();
