using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
}));

builder.Services.AddDbContext<DbHuoltokirjaContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbHuoltokirjaContext")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey =
				new SymmetricSecurityKey(System.Text.Encoding.UTF8
				.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

