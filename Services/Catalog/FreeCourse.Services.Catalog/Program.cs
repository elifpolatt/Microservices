using System.Reflection;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


//ICategoryService'le karsýlastýýgnda Categoryservice'den nesne ornegý getir.
builder.Services.AddScoped<ICategoryService, CategoryService>();
//ICourseService'le karsýlastýýgnda Courseservice'den nesne ornegý getir.
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//appsetting.json dosyasý ýcýndeký verý tabaný býlgýlerýný býr sýnýf ýcerýsýnden alacagýz.
//Classlarýn ýcýne gýdýp IOptions<DatabaseSettings> dýyerek bunlarý set edeerek kullanabýlýyoruz fakat dolu olarak json dosyasýndan gelmelý
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); 
builder.Services.AddSingleton<IDatabaseSettings>(sp => //service provider
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
