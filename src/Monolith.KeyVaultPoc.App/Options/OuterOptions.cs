namespace Monolith.KeyVaultPoc.App.Options;

public class OuterOptions
{
  public const string AppSettingsKey = "outer";

  public string Inner { get; set; } = string.Empty;
}
