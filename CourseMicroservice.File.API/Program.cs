using CourseMicroservice.File.API;
using CourseMicroservice.File.API.Features.File;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddCommonServiceExtension(typeof(FileAssembly));
builder.Services.AddApiVersionExtension();

builder.Services.AddAuthenticationAndAuthorizationExtension(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.AddFileGroupEndpointExtension(app.AddVersionSetExtension());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
