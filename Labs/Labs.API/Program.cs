using Labs.API.Data;
using Labs.API.Models;
using Microsoft.EntityFrameworkCore;

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
