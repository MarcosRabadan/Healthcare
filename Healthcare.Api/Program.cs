using Healthcare.Application.Mapping;
using Healthcare.Application.Services;
using Healthcare.Domain.Repositories;
using Healthcare.Infrastructure;
using Healthcare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IAlergiaRepository, AlergiaRepository>();
builder.Services.AddScoped<IAnamnesisRepository, AnamnesisRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IPrescripcionRepository, PrescripcionRepository>();

builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<AlergiaService>();
builder.Services.AddScoped<AnamnesisService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<PrescripcionService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<MedicamentoService>();
builder.Services.AddScoped<ProfesionalService>();

builder.Services.AddAutoMapper(typeof(PacienteProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AlergiaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AnamnesisProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CitaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ConsultaProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MedicamentoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProfesionalProfile).Assembly);

builder.Services.AddAutoMapper(typeof(PrescripcionProfile).Assembly);



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
