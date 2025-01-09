using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Net.Http.Headers;
using Monolith.KeyVaultPoc.App.Options;

namespace Monolith.KeyVaultPoc.App.Services;

public interface IMicrosoftGraphService
{
  GraphServiceClient GetGraphServiceClient();
}

public class MicrosoftGraphService(
  IHttpContextAccessor httpContext,
  IOptions<AzureAdOptions> azureAdOptions,
  IOptions<AzureKeyVaultOptions> azureKvOptions
) : IMicrosoftGraphService
{
  private readonly string[] Scopes = [ "User.Read" ];
  private readonly OnBehalfOfCredentialOptions CredentialOptions = new()
  {
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
  };

  public GraphServiceClient GetGraphServiceClient()
  {
    return new(
      new OnBehalfOfCredential(
        azureAdOptions.Value.TenantId,
        azureAdOptions.Value.ClientId,
        azureKvOptions.Value.AzureClientSecret,
        httpContext.HttpContext?.Request.Headers[HeaderNames.Authorization][0]?.Split(' ')[1],
        CredentialOptions
      ),
      Scopes
    );
  }
}
