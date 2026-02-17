using Microsoft.EntityFrameworkCore;
using WebAppNotes.DataAccess.DI;
using WebAppNotes.Application.DI;
using WebAppNotes.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
