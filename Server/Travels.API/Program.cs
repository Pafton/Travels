using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Travels.Application.Authentication;
using Travels.Application.Interfaces;
using Travels.Application.Services;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;
using Travels.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//---------------------AUTHETNTICATION----------------------


var jwtSettings = builder.Configuration.GetSection("Authentication");
var key = Encoding.ASCII.GetBytes(jwtSettings["JwtKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["JwtIssuer"],
        ValidAudience = jwtSettings["JwtIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddAuthorization();


//----------------------------------------------------------


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.EnableAnnotations();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        );
});

//SQL Server configuration ---- MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
/*
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);*/




//Adds repositories to the Dependency Injection Container
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ITransportRepository, TransportRepository>();
builder.Services.AddScoped<ITravelOfferRepository, TravelOfferRepository>();
builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();


//Adds services to the Dependency Injection Container
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITravelOfferService, TravelOfferService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IResetTokenService, ResetTokenService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IReviewService,ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();


//Password Hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// --------- LLM ------------------------------------------------------------------------------------

builder.Services.AddHttpClient<IModelLLMService, ModelLLMService>();
builder.Services.AddScoped<IModelLLMService, ModelLLMService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//----------------------------------Preparing database-----------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
    var prepDatabase = new PrepDatabase(dbContext, passwordHasher);
    //dbContext.Database.Migrate();
    prepDatabase.Seed();
}


app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
