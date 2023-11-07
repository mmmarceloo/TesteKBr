using Microsoft.EntityFrameworkCore;
using TorneioJJ_Usuarios.Context;
using TorneioJJ_Usuarios.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura a configuração
builder.Configuration.AddJsonFile("appsettings.json");

// Obter a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");

// Registra o contexto do banco de dados com a string de conexão
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EsqueceuSenhaService>();
builder.Services.AddScoped<GerarTokenService>();


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
    options.AllowAnyOrigin(); // Permitir qualquer origem (restringir em ambiente de produção)
    options.AllowAnyMethod(); // Permitir qualquer método HTTP
    options.AllowAnyHeader(); // Permitir qualquer cabeçalho HTTP
});

app.MapControllers();

app.Run();
