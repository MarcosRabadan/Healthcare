using Healthcare.Application.Services;
using Healthcare.Domain.Repositories;
using Healthcare.Infrastructure;
using Healthcare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Healthcare.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Healthcare API", Version = "v1" });
});

builder.Services.AddDbContext<HealthcareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase")));

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddAutoMapper(typeof(PacienteProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AlergiaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AnamnesisProfile).Assembly);


var app = builder.Build();

// Configura el pipeline HTTP
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

app.UseAuthorization();

app.MapControllers();

app.Run();
