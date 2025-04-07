using EmployeeDetils_Mangement.Data;
using EmployeeDetils_Mangement.Extension;
using EmployeeDetils_Mangement.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(configuration["DefaultConnection"], option => option.EnableRetryOnFailure());
});

builder.Services.AddDependencyInjection(configuration);
builder.Services.AddJwtBearerAuthentication(configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<SwaggerBasicAuthMiddleware>();

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
