using CourseMicroservice.Bus;
using CourseMicroservice.Catalog.API;
using CourseMicroservice.Catalog.API.Features.Categories;
using CourseMicroservice.Catalog.API.Features.Courses;
using CourseMicroservice.Catalog.API.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExtension();
builder.Services.AddDatabaseServiceExtension();

builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));

builder.Services.AddApiVersionExtension();

builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);
builder.Services.AddMasstransitExtension(builder.Configuration);

var app = builder.Build();


app.AddSeedDataExtension().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception.Message : "Seed data has been saved successfully.");
});

app.AddCategoryGroupEndpointExtension(app.AddVersionSetExtension());
app.AddCourseGroupEndpointExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
