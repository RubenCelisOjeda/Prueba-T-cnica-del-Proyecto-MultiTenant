using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using pruebaAppApi.Application._2.ApplicacionService.Products;
using pruebaAppApi.Application._3.Common;
using pruebaAppApi.Common;
using pruebaAppApi.Domain._2.Repository.Products;
using pruebaAppApi.Infraestructure._1.DataAccess.Dapper.Products;
using pruebaAppApi.Infraestructure._2.Connections;
using pruebaAppApi.Infraestructure._3.Common;
using pruebaAppApi.Infraestructure._4.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [Services]

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region [IoC]

#region [Connection]
builder.Services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
#endregion

#region [Product]
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IProductsRepository, ProductsRepositoryDapper>();
#endregion

#endregion

#region [Authentication]
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,

        ValidIssuer = JwtSetting.IssuerToken,
        ValidAudience = JwtSetting.AudienceToken,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting.key)),
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

#region [Mapping]
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#endregion

#region [Initialize]

#region [Jwt]
JwtSetting.Initialize(builder.Configuration);
#endregion


#region [ConnectionString]
AppConfig.Initialize(builder.Configuration);
#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsServiceOption.MyAllowSeguridad);

app.UseMiddleware<MultiTenantMiddleware>();
    
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
