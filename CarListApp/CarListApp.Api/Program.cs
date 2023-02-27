using CarListApp.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using static LoginDto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var conn = new SqliteConnection($"Data Source=C:\\carlistdb\\carlist.db");
builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarListDbContext>();

//builder.Services.AddAuthentication(options => {
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options => {
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["JwtSettings:Audience"],
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
//    };
//});

//builder.Services.AddAuthorization(options => {
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//    .RequireAuthenticatedUser()
//    .Build();
//});

//builder.Host.UseSerilog((ctx, lc) =>
//    lc.WriteTo.Console()
//    .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());

app.MapGet("/cars/{id}", async (int id, CarListDbContext db) =>
    await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
);

app.MapPut("/cars/{id}", async (int id, Car car, CarListDbContext db) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();

    record.Make = car.Make;
    record.Model = car.Model;
    record.Vin = car.Vin;

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/cars/{id}", async (int id, CarListDbContext db) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();
    db.Remove(record);
    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapPost("/cars", async (Car car, CarListDbContext db) =>
{
    await db.AddAsync(car);
    await db.SaveChangesAsync();

    return Results.Created($"/cars/{car.Id}", car);

});

app.MapPost("/login", async (LoginDto loginDto, UserManager<IdentityUser> _userManager) =>
{
    var user = await _userManager.FindByNameAsync(loginDto.Username);

    if (user is null)
    {
        return Results.Unauthorized();
    }

    var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

    if (!isValidPassword)
    {
        return Results.Unauthorized();
    }

    var response = new AuthResponseDto
    {
        UserId = user.Id,
        Username = user.UserName,
        Token = "accessToken"
    };

    return Results.Ok(response);
});

app.Run();

internal class LoginDto
{
    public string Username
    {
        get; set;
    }
    public string Password
    {
        get; set;
    }

    internal class AuthResponseDto
    {
        public string UserId
        {
            get; set;
        }
        public string Username
        {
            get; set;
        }
        public string Token
        {
            get; set;
        }
    }
}