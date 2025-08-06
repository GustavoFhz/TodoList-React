using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Profiles;
using TodoList.Services;
using TodoList.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITarefaInterface, TarefaService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProfileAutoMapper>();
});

// Configura o CORS para permitir qualquer origem (para teste, pode restringir depois)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilita CORS aqui, antes da autorização e rotas
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
