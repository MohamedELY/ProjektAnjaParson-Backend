using Microsoft.EntityFrameworkCore;
using ProjektAnjaParson_Backend.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApdatabaseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
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

//app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();



app.Run();

