using Microsoft.Extensions.DependencyInjection;
using WebAPIWithCoreMvc.ApiServices.Interfaces;
using WebAPIWithCoreMvc.ApiServices;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddHttpClient();
#region HttpClient

builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:44348/api/");
});
builder.Services.AddHttpClient<IUserApiService, UserApiService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:44348/api/");
});
#endregion
#region Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,opt =>
{
    opt.LoginPath = "/AdminPanel/Auth/Login";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "mvccookie";
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //AdminPanel/Home/Index
    endpoints.MapAreaControllerRoute(
      areaName : "AdminPanel",
      name: "AdminPanel",
      pattern: "AdminPanel/{controller=Home}/{action=Index}/{id?}"
    );
    //home/Index
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();