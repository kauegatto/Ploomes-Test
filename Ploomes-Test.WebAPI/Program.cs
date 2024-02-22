using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Mappers;
using Ploomes_Test.Domain.Service;
using Ploomes_Test.Infrastructure.Data;
using Ploomes_Test.Infrastructure.Mappers;
using Ploomes_Test.Infrastructure.Repository;
using Ploomes_Test.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ploomes Test - Tickets", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"Ploomes-Test.WebAPI.xml"), true);
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));

});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ProblemDetailsModelStateFilter));
});
builder.Services.AddSingleton<ITicketMapper, TicketMapper>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<TicketService>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ploomes Test - Tickets");
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
