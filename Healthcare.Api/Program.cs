using Healthcare.Application.Mapping;
using Healthcare.Application.Services;
using Healthcare.Application.Services.Login;
using Healthcare.Application.Settings;
using Healthcare.Domain.Repositories;
using Healthcare.Infrastructure;
using Healthcare.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAuthorization();

// Configuración de Swagger con soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Healthcare API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el campo: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<HealthcareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase")));

// Repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IAlergiaRepository, AlergiaRepository>();
builder.Services.AddScoped<IAnamnesisRepository, AnamnesisRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IPrescripcionRepository, PrescripcionRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();

// Servicios de dominio
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<AlergiaService>();
builder.Services.AddScoped<AnamnesisService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<PrescripcionService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<MedicamentoService>();
builder.Services.AddScoped<ProfesionalService>();
builder.Services.AddScoped<UsuarioService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(PacienteProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AlergiaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AnamnesisProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CitaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ConsultaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MedicamentoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProfesionalProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PrescripcionProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthcare API v1");
        c.RoutePrefix = "swagger";
    });
}

// Si solo usas HTTP, puedes comentar la siguiente línea
// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
