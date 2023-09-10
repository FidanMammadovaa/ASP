﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using ASP_Project.Services.Classes;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<User>();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddIdentity<User, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<UserContext>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
