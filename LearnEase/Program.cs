using System.Reflection;
using FluentValidation;
using LearnEase.Data;
using LearnEase.Middlewares;
using LearnEase.Repositories.EfCore;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services;
using LearnEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LearnEaseDbContext>(
    (optionsBuilder) => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
);

builder.Services.AddScoped<IUserRepository, UserEfCoreRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ILogRepository, LogEfCoreRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<LogMiddleware>();

builder.Services.AddScoped<ICourseRepository, CourseEfCoreRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IFeedbackRepository, FeedbackEfCoreRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
app.UseMiddleware<LogMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
