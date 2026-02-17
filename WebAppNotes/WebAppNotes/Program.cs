using Microsoft.EntityFrameworkCore;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;
using WebAppNotes.DataAccess.Repositories;
using WebAppNotes.DataAccess.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello WebAppNotes!");

app.Run();
