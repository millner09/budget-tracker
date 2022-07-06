using api.Data;
using api.Features.Categories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
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

    services.AddIdentity<IdentityUser, IdentityRole>(opt => opt.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<BudgetTrackerContext>();
    services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
        {
            opts.TokenValidationParameters.ValidateAudience = false;
            opts.TokenValidationParameters.ValidateIssuer = false;
            opts.TokenValidationParameters.IssuerSigningKey
            = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["BearerTokens:Key"]));
        });

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
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}