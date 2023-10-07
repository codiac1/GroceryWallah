using GroceryWallah.BusinessLayer.IServices;
using GroceryWallah.BusinessLayer.Services;
using GroceryWallah.DataAccessLayer.Data;
using GroceryWallah.DataAccessLayer.IRepository;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DataAccessLayer.Repositories;
using GroceryWallah.DataAccessLayer.Repository;
using GroceryWallah.DTO;
using GroceryWallah.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPasswordHasher<UserDto>, PasswordHasher<UserDto>>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Check if there is no admin user
        if (!context.Users.Any(u => u.IsAdmin))
        {
            // Seed the admin user
            context.Users.Add(new User
            {
                UserId = Guid.NewGuid(),
                FullName = "Admin",
                Email = "admin@mail.com",
                Phone = "1234567890",
                Password = "AQAAAAIAAYagAAAAEITtxmxhq3RpXo55hAD5CBTEhfpa/VBAacPV1xqSkhQ7TZcLIm8IuNMAgHwYsLoA0A==",
                IsAdmin = true
            });

            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Handle any errors that occur during the seeding process
        Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
    }
}

app.Run();
