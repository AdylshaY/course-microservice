using CourseMicroservice.Web.Extensions;
using CourseMicroservice.Web.Pages.Auth.SignIn;
using CourseMicroservice.Web.Pages.Auth.SignUp;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddMvc(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddOptionsExtension();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddHttpClient<TokenService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(configureOptions =>
    {
        configureOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.Cookie.Name = "CourseMicroserviceWebCookie";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
