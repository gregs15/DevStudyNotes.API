using DevStudyNotes.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
//    c =>
//{
//    c.SwaggerDoc("docs-v1", new OpenApiInfo
//    {
//        Title = "DevStudyNotes.API",
//        Version = "v1",
//        Contact = new OpenApiContact
//        {
//            Name = "Gregorio Pontim",
//            Email = "gregoriopontim@hotmail.com",
//            Url = new Uri("https://github.com/gregs15")
//        }

//    });

    //var xmlFile = "DevStudyNotes.API.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
);




var connectionString = builder.Configuration.GetConnectionString("DevStudyNotes");

builder.Services.AddDbContext<StudyNoteDbContext>(
    options => options.UseSqlServer(connectionString)
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
