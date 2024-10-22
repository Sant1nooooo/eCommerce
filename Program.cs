using Microsoft.AspNetCore.Authentication.JwtBearer;
using Server.Application.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Infrastructure.Context;
using Server.Application.Extensions;
using Server.Infrastructure.Auth;
using FluentValidation;
using System.Text;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenWithAuth();


            //This register the DbContext, MediatR, FluentValidation, JWT, ValidationPipelineBehavior as CUSTOMIZED services to your program. In which they can be access through dependency injection.
            
            //Database Context
            builder.Services.AddDbContext<ECommerceDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceConnectionString")));

            //MedaitR Library
            builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //Custom Repository
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRespository>();

            //FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            //JWT (TokenGenerator)
            builder.Services.AddSingleton<TokenGenerator>(); // To be able to use the `TokenGenerator` class through dependency injection.

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //ValidationPipelineBehavior
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>)); //Toplogic
            //PROBLEM: Hindi ma control yung behavior sa pipeline.


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
        }
    }
}

/*
    
 */