

using ConfigurationManager.Reader;

var builder = WebApplication.CreateBuilder(args);
var dbConnString = builder.Configuration.GetConnectionString("DefaultConnection");
var redisConnString = builder.Configuration.GetConnectionString("RedisConnection");
// Add services to the container.
builder.Services.AddSingleton<IConfigurationReader>(x => new ConfigurationReader("477651b0e3294e4ab1802c2d0143aa91", redisConnString, new TimeSpan(0, 10, 50), dbConnString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
