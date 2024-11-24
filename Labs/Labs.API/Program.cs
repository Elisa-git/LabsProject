using System.Text;
using Labs.API._2___Application;
using Labs.API._2___Application.Interfaces;
using Labs.API._2___Application.Profiles;
using Labs.API._4___Infra;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Origem permitida
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

//JWT
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
            ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        }
    );

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LABS API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espaço] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
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
          new string[] {}
       }
    });
});


// Services
var connectionString = builder.Configuration.GetConnectionString("ConnectionMysql");
builder.Services.AddDbContext<LabsDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddIdentityApiEndpoints<PessoaComAcesso>().AddEntityFrameworkStores<LabsDBContext>();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Camada Aplicacao e Infra
builder.Services.AddScoped<IAuthApplication, AuthApplication>();
builder.Services.AddScoped<IProdutoApplication, ProdutoApplication>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Email
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddSendGrid(options =>
    options.ApiKey = builder.Configuration.GetValue<string>("SendGridKey")
                     ?? throw new Exception("The 'SendGridApiKey' is not configured")
);
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Registro do AutoMapper
builder.Services.AddAutoMapper(typeof(AuthProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorização"); 

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
