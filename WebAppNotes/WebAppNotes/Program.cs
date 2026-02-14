using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;
using WebAppNotes.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

var app = builder.Build();

app.MapGet("/", () => "Hello WebAppNotes!");

app.Run();
