using ECommerceSystem.Controllers;
using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Data.Repositories;
using ECommerceSystem.Service;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Service.Services;
using ECommerceSystem.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ECommerceDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine,LogLevel.Information));

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


builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

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
app.UseDefaultFiles(); // Varsayýlan dosya olarak 'index.html' kullanýlýr
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
