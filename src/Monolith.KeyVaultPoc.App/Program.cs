using Monolith.KeyVaultPoc.App.Extensions;
using Monolith.KeyVaultPoc.App.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

builder.Configuration.ConfigureAzureKeyVault();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/test", () =>
{
    var value = new OuterOptions();
    builder.Configuration.Bind(OuterOptions.AppSettingsKey, value);

    return value;
})
.WithName("Test")
.WithOpenApi();

app.MapGet("/test2", () =>
{
    return builder.Configuration.GetValue<string>("test");
})
.WithName("Test2")
.WithOpenApi();

app.Run();
