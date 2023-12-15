var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.AddApplicationServices();
builder.AddAuthenticationService();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await InitializeContextAsync();
app.Run();

async Task InitializeContextAsync()
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        await ContextInitializer.InitializeAsync(context);
    }
    catch(Exception e)
    {
        Console.WriteLine("Failed to initialize database because of: " + e.Message);
    }
}