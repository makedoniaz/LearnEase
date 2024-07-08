using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Claims;
using FluentValidation;
using LearnEase.Core.Data;
using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using LearnEase.Core.Services;
using LearnEase.Infrastructure.Middlewares;
using LearnEase.Infrastructure.Repositories.EfCore;
using LearnEase.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LearnEaseDbContext>(
    (optionsBuilder) => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);

builder.Services.AddIdentity<User, IdentityRole>(options => {
    //options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<LearnEaseDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Login";
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("NotMuted", policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("IsMuted", "False");
    });

builder.Services.AddScoped<IAdminPageService, AdminPageService>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<ILogRepository, LogEfCoreRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<LogMiddleware>();

builder.Services.AddScoped<ICourseRepository, CourseEfCoreRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IFeedbackRepository, FeedbackEfCoreRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    await roleService.SetupRolesAsync();
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LogMiddleware>();

app.Run();
