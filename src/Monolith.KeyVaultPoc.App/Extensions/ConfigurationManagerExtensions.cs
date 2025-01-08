using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Monolith.KeyVaultPoc.App.Options;

namespace Monolith.KeyVaultPoc.App.Extensions;

public static class ConfigurationManagerExtensions
{
  public static ConfigurationManager ConfigureAzureKeyVault(this ConfigurationManager manager)
  {
    var options = new AzureKeyVaultOptions();
    manager.Bind(AzureKeyVaultOptions.AppSettingsKey, options);

    manager.AddAzureKeyVault(
      new Uri($"https://{options.KeyVaultName}.vault.azure.net/"),
      new ClientSecretCredential(options.AzureADDirectoryId, options.AzureADApplicationId, options.AzureClientSecret)
    );

    return manager;
  }
}
