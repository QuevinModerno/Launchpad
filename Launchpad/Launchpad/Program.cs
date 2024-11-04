
using Agap2It.Launchpad.Business.BusinessObjects;
using Data.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var connectionStrings = builder.Configuration.GetConnectionString("Launchpad");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ProjectManagementContext>(options =>
{
    options.UseSqlServer(connectionStrings);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IErc20CodeGenerationBusinessObject, Erc20CodeGenerationBusinessObject>();
builder.Services.AddScoped<IErc20CodePublisherBusinessObject, Erc20CodePublisherBusinessObject>();

//Allow requests from frontend origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var serviceFactory = (IServiceScopeFactory)app!.Services!.GetService(typeof(IServiceScopeFactory))!;
    using var scope = serviceFactory!.CreateScope();
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ProjectManagementContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin"); // AQUI TB


app.MapControllers();

app.Run();
