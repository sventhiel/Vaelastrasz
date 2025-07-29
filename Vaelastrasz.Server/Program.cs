using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Vaelastrasz.Server.Authentication;
using Vaelastrasz.Server.Filters;
using Vaelastrasz.Server.Middleware;
using Vaelastrasz.Server.Resolvers;

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "BASIC";
    options.DefaultChallengeScheme = "BASIC";
})
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("Basic", null)
    .AddPolicyScheme("BASIC", "BASIC", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            //string authorization = context.Request.Headers[HeaderNames.Authorization];

            //if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Basic ", StringComparison.InvariantCultureIgnoreCase))
            //    return "Basic";

            //return JwtBearerDefaults.AuthenticationScheme;

            return "Basic";
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basicSecurityScheme = new OpenApiSecurityScheme
    {
        Name = "Basic",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Basic",
        Description = "Provide your username and password.",

        Reference = new OpenApiReference
        {
            Id = "Basic",
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
    options.SchemaFilter<EnumSchemaFilter>();
    options.OperationFilter<AuthorizeHeaderOperationFilter>();
    options.OperationFilter<SwaggerCustomHeaderOperationFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DataCite DOI Proxy",
        Description = "A proxy service for BEXIS2 instances to communicate with DataCite.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Sven Thiel",
            Email = "m6thsv2@googlemail.com",
            Url = new Uri("https://github.com/sventhiel/vaelastrasz"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under OpenApiLicense",
            Url = new Uri("https://example.com/license"),
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB limit
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/swagger-ui/theme-flattop.css");
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "";
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


// Map controllers
app.MapControllers();

app.Run();