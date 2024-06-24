using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using Vaelastrasz.Server.Authentication;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Filters;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
.Build();

var jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.SchemaFilter<EnumSchemaFilter>();
    options.OperationFilter<AuthorizeHeaderOperationFilter>();
});

builder.Services.AddExceptionless(options =>
{
    options.ServerUrl = "https://idiv-exceptionless.fmi.uni-jena.de";
    options.ApiKey = "iwkavWY6hTVwQpnv6imSfp6fZJps70YzfIdI3mOI";
    options.IncludeUserName = true;
    options.IncludeIpAddress = true;
    options.SetVersion("v1.0");
});

// LiteDB
builder.Services.AddSingleton(new ConnectionString(builder.Configuration["ConnectionStrings:Vaelastrasz"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "BASIC_OR_JWT";
    options.DefaultChallengeScheme = "BASIC_OR_JWT";
})
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("Basic", null)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = jwtConfiguration.ValidateIssuer,
            ValidateAudience = jwtConfiguration.ValidateAudience,
            ValidAudience = jwtConfiguration.ValidAudience,
            ValidIssuer = jwtConfiguration.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey ?? ""))
        };
    })
    .AddPolicyScheme("BASIC_OR_JWT", "BASIC_OR_JWT", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            //string authorization = context.Request.Headers[HeaderNames.Authorization];

            //if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Basic ", StringComparison.InvariantCultureIgnoreCase))

            //return JwtBearerDefaults.AuthenticationScheme;
            return "Basic";
        };
    });

builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "";
});

app.UseExceptionless();

app.Run();