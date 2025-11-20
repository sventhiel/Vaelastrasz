using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using Scalar.AspNetCore;



//using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Resolvers;
using Vaelastrasz.Server.Authentication;
//using Vaelastrasz.Server.Filters;
using Vaelastrasz.Server.Middleware;
//using Vaelastrasz.Server.Transformers;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
.Build();

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// LiteDB
builder.Services.AddSingleton(new ConnectionString(builder.Configuration["ConnectionStrings:Vaelastrasz"]));

// Exceptionless
builder.Services.AddExceptionless(options =>
{
    options.ServerUrl = builder.Configuration["Exceptionless:ServerUrl"] ?? "";
    options.ApiKey = builder.Configuration["Exceptionless:ApiKey"] ?? "";
    options.IncludeUserName = Convert.ToBoolean(builder.Configuration["Exceptionless:IncludeUserName"]);
    options.IncludeIpAddress = Convert.ToBoolean(builder.Configuration["Exceptionless:IncludeIpAddress"]);
    options.SetVersion(builder.Configuration["Exceptionless:Version"] ?? "v1.0");
});

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ContractResolver = new VaelastraszContractResolver();
    o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
}).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthentication("basicAuth").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("basicAuth", null);
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {   
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["basicAuth"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            Description = "Basic Authentication"
        };

        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (Convert.ToBoolean(builder.Configuration["Exceptionless:Enabled"]))
{
    app.UseExceptionless();
    ExceptionlessClient.Default.CreateLog("Vaelastrasz.Server started")
        .SetProperty("Version", builder.Configuration["Exceptionless:Version"] ?? "v1.0")
        .Submit();
}

app.MapOpenApi().AllowAnonymous();
app.MapScalarApiReference().AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();