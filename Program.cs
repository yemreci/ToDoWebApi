using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Domain;
using ToDoApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoAppContext>(options =>
{
    options.UseInMemoryDatabase("ToDoList");
});

builder.Services.AddTransient<IToDoOperations, ToDoOperations>();
builder.Services.AddTransient<IToDoListOperations, ToDoListOperations>();
builder.Services.AddTransient<IToDoRepository, ToDoInMemoryDBRepository>();
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
