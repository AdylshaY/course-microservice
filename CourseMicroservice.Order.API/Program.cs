using CourseMicroservice.Bus;
using CourseMicroservice.Order.API.Endpoints.Orders;
using CourseMicroservice.Order.Application;
using CourseMicroservice.Order.Application.Contracts.Repositories;
using CourseMicroservice.Order.Application.Contracts.UnitOfWork;
using CourseMicroservice.Order.Persistence;
using CourseMicroservice.Order.Persistence.Repositories;
using CourseMicroservice.Order.Persistence.UnitOfWork;
using CourseMicroservice.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersionExtension();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(OrderApplicationAssembly));
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);
builder.Services.AddCommonMasstransitExtension(builder.Configuration);

var app = builder.Build();

app.AddOrderGroupEndpointExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
