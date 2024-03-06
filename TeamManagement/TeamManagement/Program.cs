using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeamDemo.Services;
using TeamManagement.Interfaces;
using TeamManagement.Repository;
using TeamManagement.Services;
using TeamManagement_Models.Database;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

#region DBContext
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection"));
});
#endregion



#region JWTAuthentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Services 
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IMailServices, MailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICaptainService, CaptainService>();
builder.Services.AddScoped<ICoachService, CoachService>();
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
