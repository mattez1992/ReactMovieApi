using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ReactMovieApi.APIBehaviour;
using ReactMovieApi.Data;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.Filters;
using ReactMovieApi.MapperProfiles;
using ReactMovieApi.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(CustomExceptionFilter));
    opt.Filters.Add(typeof(ParseBadRequestFilter));
}).AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).ConfigureApiBehaviorOptions(BadRequestBehaviour.Parse);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCaching();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOpt => sqlOpt.UseNetTopologySuite());
});



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// inject GeometryFactory to MovieTheaterProfile to enable mapping NetTopologySuite classes to regular classes.
builder.Services.AddSingleton(provider => new MapperConfiguration(conf =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    conf.AddProfile(new MovieTheaterProfile(geometryFactory));
    conf.AddProfile(new GenreProfiles());
    conf.AddProfile(new ActorProfile());
    conf.AddProfile(new MovieProfile());
    conf.AddProfile(new MovieActorProfile());
    conf.AddProfile(new UserProfile());
}).CreateMapper());
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddScoped<IFileStorageService, AzureStorageService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<CustomActionFilter>();
builder.Services.AddCors(opt =>
{
    var frontEndURL = builder.Configuration.GetValue<string>("frontend_url");
    opt.AddDefaultPolicy(b =>
    {
        b.WithOrigins(frontEndURL).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders(new string[] { "totalAmountOfrecords" });
    });
});

// identity configs
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"])),
            ClockSkew = TimeSpan.Zero
        };
        // this is the same as JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        opt.MapInboundClaims = false;
    }
    );
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsAdmin", p => p.RequireClaim("role", "admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

