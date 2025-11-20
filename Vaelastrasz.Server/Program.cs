using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Resolvers;
using Vaelastrasz.Server.Authentication;
using Vaelastrasz.Server.Middleware;

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
});

builder.Services.Configure<JsonOptions>(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB limit
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use Scalar middleware
app.MapScalarApiReference(options =>
{
    options.Title = "This is my Scalar API";
    options.DarkMode = true;
    options.Favicon = "path";
    options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.RestSharp);
    options.HideModels = false;
    options.Layout = ScalarLayout.Modern;
    options.ShowSidebar = true;

});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (Convert.ToBoolean(builder.Configuration["Exceptionless:Enabled"]))
{
    app.UseExceptionless();
    ExceptionlessClient.Default.CreateLog("Vaelastrasz.Server started")
        .SetProperty("Version", builder.Configuration["Exceptionless:Version"] ?? "v1.0")
        .Submit();
}

app.MapControllers();

app.Run();