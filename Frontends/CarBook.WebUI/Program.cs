using CarBook.WebUI.Handlers.IdentityServerHandlers;
using CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices;
using CarBook.WebUI.Services.IdentityServices.LoginServices;
using CarBook.WebUI.Services.IdentityServices.RegisterServices;
using CarBook.WebUI.Utilities.Extentions;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.LogoutPath = "/Logout/Index";
        opt.AccessDeniedPath = "/Error/Unauthorized";
        opt.Cookie.Name = "CarBookCookie";
        opt.ExpireTimeSpan = TimeSpan.FromHours(1);
        opt.SlidingExpiration = true;
    });

// Custom HttpClient Configurations
ApiSettings apiBaseUrl = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
string identityServerBaseUrl = builder.Configuration.GetSection("IdentityServerUrl").Get<string>();
builder.Services.AddHttpClientConfiguration(apiBaseUrl.ApiBaseUrl, identityServerBaseUrl);

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Extention sýnýf yazýlacak.
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IClientCredentialTokenService, ClientCredentialTokenService>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller}/{action=Index}/{id?}"
    );
});

app.Run();