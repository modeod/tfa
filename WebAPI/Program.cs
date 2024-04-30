using Application.JobTitleA;
using Application.JobTitleA.Contracts;
using Infrastructure;
using Infrastructure.Repositoies;
using Microsoft.EntityFrameworkCore;
using WebAPI.Middleware;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();
builder.Services.AddScoped<IJobTitleService, JobTitleService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Or UseExceptionHandler

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
