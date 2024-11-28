
using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Models;
using WebShop.Infrastructure.DataAccess;
using WebShop.Infrastructure.Notifications.Factory;
using WebShop.Infrastructure.Notifications.SubjectManager;
using WebShop.Infrastructure.Notifications.Subjects;
using WebShop.Infrastructure.Repositories.Factory;
using WebShop.Infrastructure.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionsString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebShopDbContext>(options => options.UseSqlServer(connectionsString));

builder.Services.AddControllers();
// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISubjectManager, SubjectManager>();

builder.Services.AddSingleton<ISubject<Product>, ProductSubject>();

//TODO TAAAAA bort?
//builder.Services.AddTransient<INotificationObserver<Product>, EmailSenderObserver>();
//builder.Services.AddTransient<INotificationObserver<Product>, TextMessageSenderObserver>();
//builder.Services.AddTransient<INotificationObserver<Product>, PushMessageSenderObserver>();

builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddTransient<ISubjectFactory, SubjectFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WebShopDbContext>();
    dbContext.Database.Migrate();
}

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
