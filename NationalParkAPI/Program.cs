using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NationalParkAPI.Data;
using NationalParkAPI.Mapper;
using NationalParkAPI.Repository;
using NationalParkAPI.Repository.IRepository;
using AutoMapper;
using System.Reflection;
using TrailAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);
//For Entity
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();

builder.Services.AddAutoMapper(typeof(NationalParkMappings));
//builder.Services.AddSwaggerGen("nameOfUrlWhereTheApiCanBeFound", write information about d API);
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("NationalParkOpenAPI",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "NationalPark API",
            Version = "1",
            Description = "Udemy Parky API",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Email = "bhrugen.udemy@gmail.com",
                //Name = "Bhrugen Patel",
                Name = "Olaoluwa Esan",
                //Url = new Uri("https://www.bhrugen.com")
                Url = new Uri("https://olasquare202.github.io/Boostrap-V5-Project-with-SASS/")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            {
                Name = "MIT License",
                Url = new Uri("https://en.wikipedia.org/wiki/MIT License")
            }
        });
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var cmlCommentsFullFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    options.IncludeXmlComments(cmlCommentsFullFullPath);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/NationalParkOpenAPI/swagger.json", "NationalParkAPI");
        options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
//Add SwashBukle to d request pipeline



app.UseAuthorization();

app.MapControllers();

app.Run();
