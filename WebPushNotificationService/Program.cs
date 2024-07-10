using WebPushNotificationService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlite3();
builder.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.Build());

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
         .WithOrigins(
            builder.Configuration["allowOrigins:githublink"] ?? throw new NullReferenceException(),
            builder.Configuration["allowOrigins:locallink"] ?? throw new NullReferenceException(),
            builder.Configuration["allowOrigins:ducklink"] ?? throw new NullReferenceException()
         )
        .WithExposedHeaders("X-Pagination")
           );
    Console.WriteLine(builder.Configuration["allowOrigins:githublink"]);
    Console.WriteLine(builder.Configuration["allowOrigins:locallink"]);
    Console.WriteLine(builder.Configuration["allowOrigins:ducklink"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapPushNotificationApi();


app.Run();


