using Hangfire;
using Microsoft.EntityFrameworkCore;
using Todo_List__API.DatabaseTables;
using Todo_List__API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This is for AutoMapper initialization
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// This is the connection for DB connection
// DefaultConnection is this connection in appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHangfireServer(); // Enables Hangfire background job processing


var app = builder.Build();

// Apply any pending migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Applies migrations automatically on startup
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ? Enable Hangfire Dashboard for job monitoring
app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire"); // Dashboard available at /hangfire

app.MapControllers();

app.Run();
