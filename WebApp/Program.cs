using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllers();
builder.Services.AddSingleton<PasswordService>();

var app = builder.Build();

//middleware
app.UseAuthorization();

app.MapControllers();

app.Run();