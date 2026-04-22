<<<<<<< HEAD
using projectBackend.Models;
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore;
using projectBackend.Models;
>>>>>>> origin/Quan

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ThuexemayContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"))
<<<<<<< HEAD
    );
=======
);
>>>>>>> origin/Quan
builder.Services.AddScoped(typeof(ThuexemayContext));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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