using Games.Domain.Models.Configuration;
using Games.Services;
using GamesApi.Domain.Constants;
using GamesApi.Rawg.Services;
using GamesApi.UsersRepository;
using GamesApi.WebApi.Middlewares;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

// Add logging to the builder
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var baseurl = builder.Configuration.GetValue<string>("RAWGSettings:BaseUrl");

builder.Services.AddHttpClient(SettingsContants.RawgHttClientName, client =>
{
    client.BaseAddress = new Uri($"{baseurl}");
})
    .AddTransientHttpErrorPolicy(p =>
      p.WaitAndRetryAsync(
        3,
        attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));

BusinessServicesConnector.Configure(builder.Services);
RawgServicesConnector.Configure(builder.Services);
RepositoryConnectors.Configure(builder.Services);


builder.Services.Configure<RAWGSettings>(builder.Configuration.GetSection(nameof(RAWGSettings)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseExceptionHandlingMiddleware();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
