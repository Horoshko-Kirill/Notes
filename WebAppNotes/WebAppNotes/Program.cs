using Microsoft.EntityFrameworkCore;
using WebAppNotes.DataAccess.DI;
using WebAppNotes.Application.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello WebAppNotes!");

app.Run();
