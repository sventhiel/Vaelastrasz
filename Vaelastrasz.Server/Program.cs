using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;
using Vaelastrasz.Server.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basicSecurityScheme = new OpenApiSecurityScheme
    {
        Name = "Basic",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Basic",
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

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

    //options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.OperationFilter<AuthorizeHeaderOperationFilter>();
});

builder.Services.AddExceptionless(options =>
{
    options.ServerUrl = "https://idiv-exceptionless.fmi.uni-jena.de";
    options.ApiKey = "RFRSCRxpd4lnyjtLagWd8U0MUc3o4ahOiQhsMgs2";
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
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = "lkjlk",
            ValidIssuer = "lklH",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sadasd"))
        };
    })
    .AddPolicyScheme("BASIC_OR_JWT", "BASIC_OR_JWT", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            string authorization = context.Request.Headers[HeaderNames.Authorization];

            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Basic ", StringComparison.InvariantCultureIgnoreCase))
                return "Basic";

            return JwtBearerDefaults.AuthenticationScheme;
        };
    });

builder.Services.AddControllers();

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
    options.RoutePrefix = "swagger/api";
});

app.UseExceptionless();

app.Run();