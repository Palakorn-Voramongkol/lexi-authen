var builder = WebApplication.CreateBuilder(args);

// Register any shared services or configurations
// e.g., Dependency injection, shared services, etc.
builder.Services.AddControllers(); // Register if controllers are part of the library

// Add any other service registrations required by the parent project
// e.g., builder.Services.AddSingleton<IMyService, MyService>();

// Build the service provider
var app = builder.Build();

// If needed for testing purposes or running the project independently, you can include minimal configurations
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Optional: Enable detailed error pages for debugging
}

// Run the app only if you intend to execute this as a standalone service (optional)
app.Run();
