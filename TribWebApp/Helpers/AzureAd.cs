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