using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}"); // works with the ErrorController

app.UseSwaggerDocumentation();

//app.UseSwagger();
//app.UseSwaggerUI(); //moved to swagger extension class

app.UseStaticFiles(); // this takes care of the wwwroot which we need for our angular app
app.UseStaticFiles(new StaticFileOptions  //this will show the new path of the image static file.
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Content")),
    RequestPath = "/Content"
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback"); //this will tell our API what to do for route it doesn't know about. This means fallback to an index method inside Fallback controller

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>(); //this migrate our store db
var identityContext = services.GetRequiredService<AppIdentityDbContext>(); // this migrate our identity db
var userManager = services.GetRequiredService<UserManager<AppUser>>(); // this for user manager
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context); //this seed the data to the store db just migrated.
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager); //this seed the data to the identity db just migrated.
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
