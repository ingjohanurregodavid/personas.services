using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using personas.services.Application.Interfaces;
using personas.services.Application.Services;
using personas.services.Infrastructure.Data;
using personas.services.Infrastructure.Repositories;
using personas.services.Infrastructure.Repositories.Interfaces;
using personas.services.Infrastructure.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Obtener la sección "JwtSettings" del archivo de configuración
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

//Se adiciona configuración JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddAuthorization();


//Politicas cors
var corsSettings = builder.Configuration.GetSection("Cors");
builder.Services.AddCors(options =>
{
    options.AddPolicy("DynamicCorsPolicy", policy =>
    {
        var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
        var allowAnyHeader = corsSettings.GetValue<bool>("AllowAnyHeader");
        var allowAnyMethod = corsSettings.GetValue<bool>("AllowAnyMethod");
        var allowCredentials = corsSettings.GetValue<bool>("AllowCredentials");

        // Aplica los orígenes permitidos desde la configuración
        policy.WithOrigins(allowedOrigins);

        // Aplica las configuraciones adicionales
        if (allowAnyHeader)
            policy.AllowAnyHeader();
        if (allowAnyMethod)
            policy.AllowAnyMethod();
        if (allowCredentials)
            policy.AllowCredentials();
    });
});

//Configuracion de la cadena de conexión por defecto
builder.Services.AddDbContext<PersonaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Personas API",
        Version = "v1"
    });

    // Configuración de autenticación JWT en Swagger
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Introduce el JWT token usando el esquema Bearer. Ejemplo: 'Bearer {tu_token}'"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();
// Aplicar la política CORS de forma global
app.UseCors("DynamicCorsPolicy");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PersonaDbContext>();
    context.Database.Migrate(); // Aplica migraciones pendientes
}
// Middleware de enrutamiento
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// Registrar middleware para el manejo de errores
app.UseMiddleware<personas.services.API.Middleware.ErrorHandlingMiddleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
