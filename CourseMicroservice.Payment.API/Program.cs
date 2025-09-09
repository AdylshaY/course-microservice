using CourseMicroservice.Payment.API;
using CourseMicroservice.Payment.API.Features.Payments;
using CourseMicroservice.Payment.API.Repositories;
using CourseMicroservice.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersionExtension();
builder.Services.AddCommonServiceExtension(typeof(PaymentAssembly));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});

builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

app.AddPaymentGroupEndpointExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
