using Microsoft.EntityFrameworkCore;
using TorneioJJ_Campeonatos.Context;
using TorneioJJ_Campeonatos.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura a configura��o
builder.Configuration.AddJsonFile("appsettings.json");

// Obter a string de conex�o do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");

// Registra o contexto do banco de dados com a string de conex�o
builder.Services.AddDbContext<CampeonatoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CampeonatoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Configurar o CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin(); // Permitir qualquer origem (restringir em ambiente de produ��o)
    options.AllowAnyMethod(); // Permitir qualquer m�todo HTTP
    options.AllowAnyHeader(); // Permitir qualquer cabe�alho HTTP
});

app.MapControllers();

app.Run();
