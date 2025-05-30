using CourseMicroservice.Basket.API;
using CourseMicroservice.Basket.API.Features.Baskets;
using CourseMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExtension(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddApiVersionExtension();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddBasketGroupEndpointExtension(app.AddVersionSetExtension());

app.Run();
