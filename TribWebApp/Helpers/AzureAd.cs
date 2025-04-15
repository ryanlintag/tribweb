public sealed class AzureAd
{
    public const string SectionName = "AzureAd";
    public string Instance { get; set; } = "https://login.microsoftonline.com/";
    public string TenantId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string CallbackPath { get; set; } = "/signin-oidc";
    public string Authority => $"{Instance}{TenantId}/v2.0";
    private string _scopes = string.Empty;
    public string Scopes 
    { 
        set 
        {
            _scopes = value;
        }
        get
        {
            return $"api://{ClientId}/{_scopes}";
        }
    }
    public string Audience 
    {
        get
        {
            return $"api://{ClientId}";
        }
    }
}
public interface IConfigService<T>{
    T GetConfig();
}
public interface IAzureConfigService : IConfigService<AzureAd> { }
public sealed class AzureConfigService : IAzureConfigService
{
    private readonly IConfiguration _configuration;
    public AzureConfigService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AzureAd GetConfig()
    {        
        // Retrieve configuration values and log them
        var clientSecret = _configuration["AzureAd:AZURE_AD_CLIENT_SECRET"];
        var clientId = _configuration["AzureAd:AZURE_AD_CLIENT_ID"];
        var tenantId = _configuration["AzureAd:AZURE_AD_TENANT_ID"];

        var azureAd = new AzureAd();
        _configuration.GetSection(AzureAd.SectionName).Bind(azureAd);
        azureAd.ClientId = clientId;
        azureAd.ClientSecret = clientSecret;
        azureAd.TenantId = tenantId;

        return azureAd;
    }
}