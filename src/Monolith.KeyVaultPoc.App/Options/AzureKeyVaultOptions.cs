namespace Monolith.KeyVaultPoc.App.Options;

public class AzureKeyVaultOptions
{
  public const string AppSettingsKey = "AzureKeyVault";

  public string KeyVaultName { get; set; } = string.Empty;
  public string AzureADDirectoryId { get; set; } = string.Empty;
  public string AzureADApplicationId { get; set; } = string.Empty;
  public string AzureClientSecret { get; set; } = string.Empty;

  // needed for cert
  public string AzureADCertThumbPrint { get; set; } = string.Empty;
}
