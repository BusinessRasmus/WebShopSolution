
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebShop.DataAccess;
using WebShop.DataAccess.Factory;
using WebShop.DataAccess.UnitOfWork;
using WebShop.Shared.Models;
using WebShop.Shared.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionsString = builder.Configuration.GetConnectionString("WebShopDb");
builder.Services.AddDbContext<WebShopDbContext>(options => options.UseSqlServer(connectionsString));

builder.Services.AddControllers();
// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<INotificationObserver<Product>, EmailNotification>();
builder.Services.AddScoped<ISubject<Product>, ProductSubject>();
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
