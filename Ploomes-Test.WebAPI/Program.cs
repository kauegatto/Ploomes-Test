using Microsoft.EntityFrameworkCore;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;
using Ploomes_Test.Infrastructure.Data;
using Ploomes_Test.Infrastructure.Mappers;
using Ploomes_Test.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));

});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSingleton<ITicketMapper, TicketMapper>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
