using Asp.Versioning;
using GameLocalization.Application.Extensions;
using GameLocalization.Configurations;
using GameLocalization.Extensions;
using GameLocalization.Infrastructure.Mapping.Extensions;
using GameLocalization.Persistence.Postgres.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region API

builder.Services.AddControllers();
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://frontend:8080", "http://localhost:8082")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

#endregion

#region Logging

builder.Services.AddSerilog(builder.Configuration);
builder.Host.UseSerilog();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "GameLocalization.xml");
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

#endregion

#region ConfigureOptions

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

#endregion

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMapping();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment()) app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthorization();

app.MapControllers();

app.Run();