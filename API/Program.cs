using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// AddScoped means that the service will live as long as the http request
// Others like AddTransient is effectivily scoped to the method level not the request level
builder.Services.AddScoped<IProductRepository,ProdcutRepository>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();


// }

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<StoreContext>();

    // It will create new database if there are no
    // And it will seed the data to the new database
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (System.Exception)
{
    throw;
}

app.Run();
