using LearnEase.Middlewares;
using LearnEase.Repositories;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services;
using LearnEase.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILogRepository, LogDapperRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<LogMiddleware>();

builder.Services.AddScoped<ICourseRepository, CourseDapperRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddSingleton<IFeedbackRepository, FeedbackDapperRepository>();
builder.Services.AddSingleton<IFeedbackService, FeedbackService>();


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
