using WorkoutTrackerWebsite.Controllers;
using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddHttpClient<WorkoutController>();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlite<WorkoutContext>("Data Source=WorkoutTracker.db");

builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<WorkoutController>();
builder.Services.AddScoped<LoggerFactory>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.CreateDbIfNotExists();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
