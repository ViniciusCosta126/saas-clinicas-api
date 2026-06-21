using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Auth;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Dtos.Professionals;
using SaasClinicas.Api.Dtos.Users;
using SaasClinicas.Api.Mappings;
using SaasClinicas.Api.Middleware;
using SaasClinicas.Api.Services;
using SaasClinicas.Api.Validators.Auth;
using SaasClinicas.Api.Validators.Clinics;
using SaasClinicas.Api.Validators.Patients;
using SaasClinicas.Api.Validators.Professionals;
using SaasClinicas.Api.Validators.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<PatientCreateDto>, PatientCreateValidator>();
builder.Services.AddScoped<IValidator<ProfessionalCreateDto>, ProfessionalCreateValidator>();
builder.Services.AddScoped<IValidator<ClinicCreateDto>, ClinicCreateValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insira o token jwt com o prefixo 'Bearer'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ClinicProfile>();
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<ProfessionalProfile>();
    cfg.AddProfile<PatientProfile>();
});
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true
    };
});
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
