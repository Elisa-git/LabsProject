using Labs.API._2___Application;
using Labs.API._2___Application.Interfaces;
using Labs.API._2___Application.Profiles;
using Labs.API._4___Infra;
using Labs.API._4___Infra.Interfaces;
using Labs.API.Config;
using Labs.API.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionMysql");
builder.Services.AddDbContext<LabsDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddIdentityApiEndpoints<PessoaComAcesso>().AddEntityFrameworkStores<LabsDBContext>();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Camada Aplicacao
builder.Services.AddScoped<IAuthApplication, AuthApplication>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorização"); 

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
