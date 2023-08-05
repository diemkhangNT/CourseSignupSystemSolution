global using GiangVienWebAPI.Data;
global using GiangVienWebAPI.Models;
global using GiangVienWebAPI.Interfaces;
global using GiangVienWebAPI.Services;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;

global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region
//cho phép mọi thiết bị kết nối đến API
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//tạo lk database
//builder.Services.AddDbContext<ApiDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("CourseSignup"));
//});
//Cách 2
/* Database Context Dependency Injection Environment.GetEnvironmentVariable("DB_HOST")*/
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var dbHost = ".";
var dbName = "dms_giangvien";
var dbPassword = "Dk1912@2002!";
var connectionString = $"Data Source={dbHost}; Initial Catalog={dbName}; User ID=sa; Password={dbPassword}";
builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(connectionString));

// Đăng ký interface IExistAlreadyService và thực hiện các chức năng của nó trong file ExistAlreadyService
builder.Services.AddScoped<IExistName, ExistNameService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
