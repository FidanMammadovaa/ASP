using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using minimalAPI.Context;
using minimalAPI.Entities;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>();

var app = builder.Build();
app.UseStaticFiles();

app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
});

app.MapGet("/getAll", async () =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    if (dbContext.Users != null)
    {
        var users = await dbContext.Users.ToListAsync();
        return Results.Ok(users);
    }
    return Results.BadRequest("No users yet");
});

app.MapGet("/getById/{id}", async (int id) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    if (dbContext.Users != null)
    {
        var user = await dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        if (user != null)
        {
            return Results.Ok(user);
        }
        return Results.BadRequest("No user was found");
    }
    return Results.BadRequest("No users yet");

});

app.MapPost("/post", async ([FromBody] User user) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password))
    {
        Regex regex = new(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+");


        var match = regex.Match(user.Email);
        if (match.Success)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Results.Ok("User was successfully added");
        }
    }
    return Results.BadRequest("Invalid email or password");
});

app.MapDelete("/delete/{id}", async (int id) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    if (dbContext.Users != null)
    {
        var user = await dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        if (user != null)
        {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return Results.Ok("User was succesfully deleted");
        }
        return Results.BadRequest("No user was found");
    }
    return Results.BadRequest("No users yet");
});

app.MapPatch("/patch/{id}", async (int id, [FromBody] User user) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    var userToChange = await dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
    if (dbContext.Users != null)
    {
        if (userToChange != null)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                Regex regex = new(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+");
                var match = regex.Match(user.Email);
                if (match.Success)
                {
                    userToChange.Email = user.Email;
                }
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                userToChange.Password = user.Password;
            }
            dbContext.Users.Update(userToChange);
            dbContext.SaveChanges();
            return Results.Ok("User was succesfully updated");
        }
        return Results.BadRequest("No user was found");
    }
    return Results.BadRequest("No users yet");
});

app.Run();
