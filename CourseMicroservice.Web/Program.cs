using CourseMicroservice.Web.Extensions;
using CourseMicroservice.Web.Pages.Auth.SignIn;
using CourseMicroservice.Web.Pages.Auth.SignUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddOptionsExtension();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
