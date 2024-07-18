using System.Reflection;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


//ICategoryService'le kars�last��gnda Categoryservice'den nesne orneg� getir.
builder.Services.AddScoped<ICategoryService, CategoryService>();
//ICourseService'le kars�last��gnda Courseservice'den nesne orneg� getir.
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper 
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//appsetting.json dosyas� �c�ndek� ver� taban� b�lg�ler�n� b�r s�n�f �cer�s�nden alacag�z.
//Classlar�n �c�ne g�d�p IOptions<DatabaseSettings> d�yerek bunlar� set edeerek kullanab�l�yoruz fakat dolu olarak json dosyas�ndan gelmel�
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
