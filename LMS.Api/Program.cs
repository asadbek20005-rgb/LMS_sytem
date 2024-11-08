using LMS.Common.JwtModels;
using LMS.Data.Context;
using LMS.Data.Repositories.Implementations;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Api;
using LMS.Service.JwtToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
//Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
//builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<ICardInfoRepository, CardInfoRepository>();
builder.Services.AddScoped<IUser_Course_PaymentRepository, User_Course_PaymentRepository>();
builder.Services.AddScoped<IUser_Course_Report, User_Course_ReportRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<JwtTokenService>();
//builder.Services.AddScoped<ContentServce>();
builder.Services.AddScoped<CardInfoService>();
builder.Services.AddScoped<User_Course_PaymentService>();
//builder.Services.AddScoped<GoogleDrive>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = securityKey,
        ClockSkew = TimeSpan.FromDays(1),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,
    };
}); 

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
