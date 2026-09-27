using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ITIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


// /Department/Details/1
app.MapControllerRoute(
      name: "DepartmentDetails",
    pattern: "Dept/{id:int}",
    defaults: new
    {
        controller = "Department",
        action = "Details"
    });


/// /Instructor/Details/1
/// /Inst/1
app.MapStaticAssets();

app.MapControllerRoute(
      name: "InstructorDetailsV2",
    pattern: "Inst/{id:int}",
    defaults: new
    {
        controller = "Instructor",
        action = "Details"
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
