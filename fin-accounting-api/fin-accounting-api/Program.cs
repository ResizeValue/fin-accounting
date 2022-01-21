using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using fin_accounting_api.Data;
using fin_accounting_api.Areas.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using fin_accounting_api;
using IdentityServer4.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("fin_accounting_apiContextConnection");
builder.Services.AddDbContext<fin_accounting_apiContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors();

builder.Services.AddIdentityServer(options =>
{
    // http://docs.identityserver.io/en/release/reference/options.html#refoptions
    options.Endpoints = new EndpointsOptions
    {
        // � Implicit Flow ������������ ��� ��������� �������
        EnableAuthorizeEndpoint = true,
        // ��� ��������� ������� ������
        EnableCheckSessionEndpoint = true,
        // ��� ������� �� ���������� ������������
        EnableEndSessionEndpoint = true,
        // ��� ��������� claims �������������������� ������������ 
        // http://openid.net/specs/openid-connect-core-1_0.html#UserInfo
        EnableUserInfoEndpoint = true,
        // ������������ OpenId Connect ��� ��������� ����������
        EnableDiscoveryEndpoint = true,

        // ��� ��������� ���������� � �������, �� �� ����������
        EnableIntrospectionEndpoint = false,
        // ��� �� ����� �.�. � Implicit Flow access_token �������� ����� authorization_endpoint
        EnableTokenEndpoint = false,
        // �� �� ���������� refresh � reference tokens 
        // http://docs.identityserver.io/en/release/topics/reference_tokens.html
        EnableTokenRevocationEndpoint = false
    };

    // IdentitySever ���������� cookie ��� �������� ����� ������
    options.Authentication = new IdentityServer4.Configuration.AuthenticationOptions
    {
        CookieLifetime = TimeSpan.FromDays(1)
    };

}).AddDeveloperSigningCredential()
        // ��� �������� � id_token
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        // ��� �������� � access_token
        .AddInMemoryApiResources(Config.GetApiResources())
        // ��������� ���������� ����������
        .AddInMemoryClients(Config.GetClients())
        // �������� ������������
        .AddTestUsers(Config.GetUsers());


builder.Services.AddIdentity<apiUser, IdentityRole>(
                    option =>
                    {
                        option.Password.RequireDigit = false;
                        option.Password.RequiredLength = 6;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                    }
                ).AddEntityFrameworkStores<fin_accounting_apiContext>()
                .AddDefaultTokenProviders();

// ��������� ��������� �������
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Site"],
        ValidIssuer = builder.Configuration["Jwt:Site"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
    };
});
// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
