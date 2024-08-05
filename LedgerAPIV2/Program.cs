using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LedgerAPIV2.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LedgerAPIV2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LedgerAPIV2Context") ?? throw new InvalidOperationException("Connection string 'LedgerAPIV2Context' not found.")));

// Add services to the container.

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
