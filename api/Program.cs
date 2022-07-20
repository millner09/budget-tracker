using api.Services;
using application.Categories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using persistance;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder);

var app = builder.Build();
ConfigureApplication(app);

app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    // Add services to the container.

    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7247");
    }));

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    services.AddScoped<TokenService>();


    services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CategoryValidator>();
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMediatR(typeof(CreateCategory.Handler).Assembly);
    services.AddAutoMapper(typeof(CreateCategory.MappingProfile).Assembly);

    var connStringBuilder = new NpgsqlConnectionStringBuilder();
    connStringBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
    connStringBuilder.Username = builder.Configuration["UserID"];
    connStringBuilder.Password = builder.Configuration["Password"];
    connStringBuilder.Host = builder.Configuration["PGSQLHostName"];

    builder.Services.AddDbContext<BudgetTrackerContext>(opt =>
    {
        opt.UseNpgsql(connStringBuilder.ConnectionString);
    });
}

static void ConfigureApplication(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseCors("MyPolicy");
    }
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}