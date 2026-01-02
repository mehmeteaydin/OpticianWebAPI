using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.Services.concretes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OpticianWebAPI.DatabaseContext;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IFrameService,FrameService>();
builder.Services.AddScoped<ILensService,LensService>();
builder.Services.AddScoped<IGlassesService,GlassesService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IExpenseService,ExpenseService>();
builder.Services.AddScoped<ISaleService,SaleService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
options.TokenValidationParameters = new TokenValidationParameters
{
   ValidateIssuer = true,
   ValidateAudience = true,
   ValidateLifetime = true,
   ValidateIssuerSigningKey = true,
   ValidIssuer = builder.Configuration["Jwt:Issuer"],
   ValidAudience = builder.Configuration["Jwt:Audience"],
   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
});

var app = builder.Build();

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
