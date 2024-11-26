
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebShop.DataAccess.DataAccess;
using WebShop.DataAccess.Repositories.Factory;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Domain.Models;
using WebShop.Infrastructure.Notifications.Observers;
using WebShop.Infrastructure.Notifications.Subjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionsString = builder.Configuration.GetConnectionString("WebShopDb");
builder.Services.AddDbContext<WebShopDbContext>(options => options.UseSqlServer(connectionsString));

builder.Services.AddControllers();
// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver<Product>, EmailNotificationObserver>();
builder.Services.AddSingleton<ISubject<Product>, ProductSubject>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
