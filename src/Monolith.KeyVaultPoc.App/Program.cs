using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Monolith.KeyVaultPoc.App.Extensions;
using Monolith.KeyVaultPoc.App.Options;
using Monolith.KeyVaultPoc.App.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

builder.Configuration.ConfigureAzureKeyVault();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(AzureAdOptions.AppSettingsKey));

builder.Services
    .Configure<AzureAdOptions>(builder.Configuration.GetSection(AzureAdOptions.AppSettingsKey))
    .Configure<AzureKeyVaultOptions>(builder.Configuration.GetSection(AzureKeyVaultOptions.AppSettingsKey))
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
    .AddScoped<IMicrosoftGraphService, MicrosoftGraphService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", async (IMicrosoftGraphService graphService) =>
{
    var value = new OuterOptions();
    builder.Configuration.Bind(OuterOptions.AppSettingsKey, value);

    var graphClient = graphService.GetGraphServiceClient();
    var user = await graphClient.Me
    .GetAsync(config =>
    {
        config.QueryParameters.Select =
        [
            "givenName",
            "surname",
            "displayName",
            "employeeId",
            "officeLocation"
        ];
    });

    var memberOf = user?.MemberOf;
    var givenName = user?.GivenName;
    var surname = user?.Surname;
    var displayName = user?.DisplayName;
    var employeeId = user?.EmployeeId;
    var officeLocation = user?.OfficeLocation;

    var transitiveMemberOf = await graphClient.Me.TransitiveMemberOf.GraphGroup.GetAsync(config => {
        config.QueryParameters.Select =
        [
            "displayName"
        ];
    });

    return value;
})
.WithName("Test")
.RequireAuthorization(policy => policy.RequireRole("ab7dad83-04a1-44bd-86ec-ac652fa30389"))
.RequireScope("Loans.ReadAndWrite")
.WithOpenApi();

app.Run();
