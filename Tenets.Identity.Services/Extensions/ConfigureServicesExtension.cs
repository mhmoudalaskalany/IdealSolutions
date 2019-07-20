﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Tenets.Common.Identity.Dto;
using Tenets.Common.Identity.Interface;
using Tenets.Identity.Data.Context;
using Tenets.Identity.Data.SeedData;

namespace Tenets.Identity.Services.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.DatabaseConfig(_configuration);
            services.JWTSettings(_configuration);
            services.Dtos();
            return services;
        }
        private static void JWTSettings(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Site"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Site"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"])),
                };
            });
        }
        private static void DatabaseConfig(this IServiceCollection services,IConfiguration _configuration)
        {
            var connection = _configuration.GetConnectionString("IdentityContext"); 
            services.AddDbContext<IdentityContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
            services.AddScoped<DbContext, IdentityContext>();
            services.AddSingleton<IDataInitialize, DataInitialize>();
        }
        private static void Dtos(this IServiceCollection services)
        {
            services.AddSingleton<IUserDto, UserDto>();
            services.AddSingleton<IUserDto, UserDto>();
            services.AddSingleton<IScreenDto, ScreenDto>();
            services.AddSingleton<IRoleDto, RoleDto>();
            services.AddSingleton<IMenuDto, MenuDto>();
            services.AddSingleton<IUserLoginReturn, UserLoginReturn>();
        }
    }
}
