using LOGIC;
using DATABASE;
using CORE;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5290");

builder.Services.AddControllers();

builder.Services.AddScoped<ILogInDB<User>, LogInDB>();

builder.Services.AddScoped<IDataToObject<User, User>, UsersDB>();

builder.Services.AddScoped<ILogInManager<OutgoingLogInDTO>, LogInServiceForDTO>();

builder.Services.AddScoped<UserServiceForDTO>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
