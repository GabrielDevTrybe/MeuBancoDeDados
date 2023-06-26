using CRUD.Services;
using MeuBancoDeDados.Services;
using MeuBancoDeDados.Services.Interfaces;
using SeuNamespace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddDbContext<MeuDbContext>();

builder.Services.AddScoped<Iusers, usersService>();
//builder.Services.AddScoped<Iindicators, indicatorsService>();
//builder.Services.AddScoped<IEvent, EventService>();
//builder.Services.AddScoped<IUserSurvey, UserSurveyService>();


// Configure o HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();