using LOGIC;
using DATABASE;
using CORE;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Lägg till denna
using Microsoft.Extensions.Configuration;
using System.Text; // Lägg till denna
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

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

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
            ),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

builder.Services.AddAuthorization();

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
