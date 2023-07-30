using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}"); // works with the ErrorController

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

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
