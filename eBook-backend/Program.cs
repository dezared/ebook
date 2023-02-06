using System.Text;
using eBook_backend;
using eBook_backend.Utils.Exceptions;
using eBook_backend.Utils.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using surfis_backend;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Surfis BackEnd API",
        Version = "v1"
    });

    //var filePath = Path.Combine(AppContext.BaseDirectory, "_api_annotation.xml");
    //config.IncludeXmlComments(filePath);

    config.AddEnumsWithValuesFixFilters();
    config.EnableAnnotations();

    //config.UseDateOnlyTimeOnlyStringConverters();

    config.OperationFilter<SwaggerOperationFilter>();
    //config.SchemaFilter<EnumTypesSchemaFilter>(filePath);
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Describe accessToken."
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("Jwt_ValidAudience") ?? "localhost",
            ValidAudience = Environment.GetEnvironmentVariable("Jwt_ValidIssuer") ?? "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? "_test_string_jwt"))
        };
    });

builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var responseObj = new
        {
            path = context.HttpContext.Request.Path.ToString(),
            method = context.HttpContext.Request.Method,
            controller = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerName,
            action = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
            errors = context.ModelState.Keys.Select(k =>
            {
                return new
                {
                    field = k,
                    Messages = context.ModelState[k]?.Errors.Select(e => e.ErrorMessage)
                };
            })
        };
        return new BadRequestObjectResult(responseObj);
    };
});


builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString") ?? "ebook-db;Database=ebook-database;Username=postgres;Password=admin;Port=5432")
    );

builder.Services.MigrateDatabase();
builder.Services.RequiredProvideServices();
builder.Services.SeedDatabase().Wait();


var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Unspecified
});

// custom middleware
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.InjectStylesheet("/ext/custom-swagger-stylesheet.css");
    config.InjectJavascript("/ext/custom-swagger-javascript.js");
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();