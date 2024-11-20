using LMS.Common.JwtModels;
using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Repositories.Implementations;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Api;
using LMS.Service.File;
using LMS.Service.Helpers;
using LMS.Service.JwtToken;
using LMS.Service.MemoryCache;
using LMS.Service.Otp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddScoped<IUser_CourseRepository, User_CourseRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<ICardInfoRepository, CardInfoRepository>();
builder.Services.AddScoped<IUser_Course_PaymentRepository, User_Course_PaymentRepository>();
builder.Services.AddScoped<IUser_Course_Report, User_Course_ReportRepository>();
builder.Services.AddScoped<IOtpRepository, OtpReposiotry>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<List<User>>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<ContentServce>();
builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<User_CourseRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<CardInfoService>();
builder.Services.AddScoped<User_Course_PaymentService>();
builder.Services.AddScoped<OtpService>();
builder.Services.AddScoped<MemoryCacheService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
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


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
