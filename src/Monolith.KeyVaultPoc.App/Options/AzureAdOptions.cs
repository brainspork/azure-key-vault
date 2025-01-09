namespace Monolith.KeyVaultPoc.App.Options;

public class AzureAdOptions
{
  public const string AppSettingsKey = "AzureAd";

  public string Instance { get; set; } = string.Empty;
  public string ClientId { get; set; } = string.Empty;
  public string TenantId { get; set; } = string.Empty;
  public string Audience { get; set; } = string.Empty;
}
