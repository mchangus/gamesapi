using Games.Domain.Models.Configuration;
using Games.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var baseurl = builder.Configuration.GetValue<string>("RAWGSettings:BaseUrl");

builder.Services.AddHttpClient("RawgClient", client =>
{
    client.BaseAddress = new Uri(baseurl);
})
    .AddTransientHttpErrorPolicy(p =>
      p.WaitAndRetryAsync(
        3,
        attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));

ServicesConnector.Configure(builder.Services);


builder.Services.Configure<RAWGSettings>(builder.Configuration.GetSection(nameof(RAWGSettings)));


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
