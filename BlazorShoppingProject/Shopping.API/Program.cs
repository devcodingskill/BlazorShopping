using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shopping.API.Data;
using Shopping.API.Repository;
using Shopping.API.Repository.Contacts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register the DbContext with the dependency injection container
builder.Services.AddDbContext<ShopOnlineDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the repository with the dependency injection container
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Enable CORS policy as we are going to access the API from a different domain
app.UseCors(policy => policy.WithOrigins("http://localhost:7070"," https://localhost:7070")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
.AllowAnyOrigin());

//app.AddCors(options =>
//    {
//        options.AddPolicy("AllowAllOrigins",
//            builder =>
//            {
//                builder.AllowAnyOrigin()
//                       .AllowAnyMethod()
//                       .AllowAnyHeader();
//            });
//    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
