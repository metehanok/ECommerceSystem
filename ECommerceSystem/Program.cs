using ECommerceSystem.Controllers;
using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Data.Repositories;
using ECommerceSystem.Service;
using ECommerceSystem.Data;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Service.Services;
using ECommerceSystem.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ECommerceDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));
//builder.Services.AddDbContext<ECommerceDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(//c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "ECommerce Management System WebAPI",
//        Version = "v1",
//        Description = "E-ticaretin kolay yolu.",
//        TermsOfService = new Uri("https://example.com.terms"),
//        Contact = new OpenApiContact
//        {
//            Name = "Metehan Ok",
//            Email = "metet.ok@outlook.com",
//            Url = new Uri("https://www.linkedin.com/in/metehanok/"),
//        },
//        License = new OpenApiLicense
//        {
//            Name = "API License",
//            Url = new Uri("https://example.com/license"),
//        }
//    });
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath);
//}
//);

builder.Services.AddAutoMapper(typeof(DTOMapper));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddFluentValidationAutoValidation(opt =>
  { opt.DisableDataAnnotationsValidation = true; }
);

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IOrderDetailsService,OrderDetailService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<ICustomerService,CustomerService>();

builder.Services.AddScoped<IValidator<CategoryCreateDTO>,CategoryCreateDTOValidator>();
builder.Services.AddScoped<IValidator<CategoryUpdateDTO>,CategoryUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<ProductCreateDTO>,ProductCreateDTOValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDTO>,ProductUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<OrderCreateDTO>,OrderCreateDTOValidator>();
builder.Services.AddScoped<IValidator<OrderUpdateDTO>,OrderUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerCreateDTO>,CustomerCreateDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerUpdateDTO>,CustomerUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<OrderDetailCreateDTO>, OrderDetailsCreateDTOValidator>();
builder.Services.AddScoped<IValidator<OrderDetailUpdateDTO>, OrderDetailUpdateDTOValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:44320", "https://ecommercesystem-1.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000, listenOptions =>
//    {
//        listenOptions.UseHttps(); // Sertifika ayarlýysa bunu kullan
//    });
//}); 

// HTTPS kaldýr ve sadece HTTP dinle:
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // HTTP olarak dinler
});



builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    context.Database.Migrate(); // Migration'larý uygular
}

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapFallbackToFile("/wwwroot/login.html");

    //app.UseSwagger();
    //app.UseSwaggerUI(//c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce Management System WebAPI v1");
    //    c.RoutePrefix = string.Empty;
    //}

}

//app.UseCors(build=>

//    build.WithOrigins("https://ecommercesystem-1.onrender.com") // Frontend URL'sini buraya ekleyin
//              .AllowAnyMethod()              
//              .AllowAnyHeader());


app.UseDefaultFiles(); // Varsayýlan dosya olarak 'index.html' kullanýlýr
app.UseStaticFiles();
app.UseRouting();

app.UseCors("FrontendPolicy");


//builder.WebHost.UseUrls("http://0.0.0.0:5001");//render için 
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
