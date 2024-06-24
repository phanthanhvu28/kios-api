using ApplicationCore;

using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using VELA.WebCoreBase.Core.Common;
using VELA.WebCoreBase.Core.Filters;
using VELA.WebCoreBase.Libraries.Constants;
using VELA.WebCoreBase.Libraries.HealthCheck;
using VELA.WebCoreBase.Libraries.Sidecar;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

GlobalConfiguration.Bind(builder.Configuration, builder.Environment);

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    DateTimeZoneHandling = DateTimeZoneHandling.Utc
};

builder.Services
    .AddControllers(p =>
    {
        p.Filters.Add(typeof(ApiExceptionFilterAttribute));
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    });

string defaultCors = "default";
builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultCors,
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Config Identity server.

//string? identityServerAddress = builder.Configuration["IdentityServer:IdentityServerUrl"];
//string? identityServerAudience = builder.Configuration["IdentityServer:IdentityServerAudience"];

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", opt =>
//    {
//        opt.RequireHttpsMetadata = false;
//        opt.Authority = identityServerAddress;
//        opt.Audience = identityServerAudience;
//    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration[Service.Name],
        Version = "v1"
    });

    c.CustomSchemaIds(x => x.FullName?.Replace("+", "."));
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
});

builder.Services.AddHttpContextAccessor();

// Application Configuration
builder.Services.AddApplicationServices(builder.Configuration);

// Infrastructure Configuration
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHealthCheck();

builder.Services.AddDapr(builder.Configuration);

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 80 * 1024 * 1024;  //80Mb, if don't set default value is: 30 MB
});

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        string? url = builder.Configuration["Service:ServerUrl"];
        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        c.PreSerializeFilters.Add((swaggerDoc, _) =>
        {
            swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = url } };
        });
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.DocExpansion(DocExpansion.None);
});


// app.UseHttpsRedirection();
app.UseCors(defaultCors);
app.UseStaticFiles();

app.UseCloudEvents();

app.MigrationDatabase();

//app.UseAuthorization();

app.MapHealthCheck();

app.MapControllers();

app.Run();