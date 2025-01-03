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

    // set up for using local cert
    // using var x509Store = new X509Store(StoreLocation.CurrentUser);

    // x509Store.Open(OpenFlags.ReadOnly);

    // var X509Certificate = x509Store.Certificates
    //   .Find(
    //     X509FindType.FindByThumbprint,
    //     options.AzureADCertThumbPrint,
    //     validOnly: false)
    //   .OfType<X509Certificate2>()
    //   .Single();

    manager.AddAzureKeyVault(
      new Uri($"https://{options.KeyVaultName}.vault.azure.net/"),
      //// Using cert
      // new ClientCertificateCredential(
      //   options.AzureADDirectoryId,
      //   options.AzureADApplicationId,
      //   X509Certificate
      // ),
      // Using service principal secret
      new ClientSecretCredential(options.AzureADDirectoryId, options.AzureADApplicationId, options.AzureClientSecret)
    );

    return manager;
  }
}
