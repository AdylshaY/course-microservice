using CourseMicroservice.Bus;
using CourseMicroservice.Discount.API;
using CourseMicroservice.Discount.API.Features.Discounts;
using CourseMicroservice.Discount.API.Options;
using CourseMicroservice.Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExtension();
builder.Services.AddDatabaseServiceExtension();

builder.Services.AddCommonServiceExtension(typeof(DiscountAssembly));

builder.Services.AddApiVersionExtension();

builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);
builder.Services.AddMasstransitExtension(builder.Configuration);

var app = builder.Build();

app.AddDiscountGroupEndpointExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
