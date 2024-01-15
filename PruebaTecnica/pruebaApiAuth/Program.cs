using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using pruebaApiAuth.Application._2.ApplicacionService.Organization;
using pruebaApiAuth.Application._2.ApplicacionService.Users;
using pruebaApiAuth.Application._3.Common;
using pruebaApiAuth.Common;
using pruebaApiAuth.Domain._2.Repository.Organization;
using pruebaApiAuth.Domain._2.Repository.Users;
using pruebaApiAuth.Infraestructure._1.DataAccess.Dapper.Organization;
using pruebaApiAuth.Infraestructure._1.DataAccess.Dapper.Users;
using pruebaApiAuth.Infraestructure._2.Connections;
using pruebaApiAuth.Infraestructure._3.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [Services]

#region [IoC]

#region [Connection]
builder.Services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
#endregion

#region [Users]
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersRepository, UsersRepositoryDapper>();
#endregion

#region [Organization]
builder.Services.AddTransient<IOrganizationService, OrganizationService>();
builder.Services.AddTransient<IOrganizationRepository, OrganizationRepositoryDapper>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
