using System;
using Bank.Api.Pipelines;
using Bank.Application.AppServices.Lancamentos;
using Bank.Domain.Lancamentos.Services;
using Bank.Infra.CrossCutting.Interfaces;
using Bank.Infra.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api
{
    public static class Setup
    {
        public static void SwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("app",
                    new Info
                    {
                        Title = "Bank API",
                        Version = "v1",
                        Description = "Exemplo de API REST criada para o teste pratico para a vaga do santander",
                        Contact = new Contact
                        {
                            Name = "Thiago Moreira",
                            Url = "https://github.com/moreirawebmaster"
                        }
                    });
            });
        }

        public static void MediatRSetup(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommand<,>));
            services.AddMediatR(typeof(ILancamentoServiceWrite).GetTypeInfo().Assembly,
                typeof(ILancamentoServiceWrite).GetTypeInfo().Assembly);
        }

        public static void IoCSetup(this IServiceCollection services)
        {
            services.AddDbContext<BankContext>();
            services.AddScoped(typeof(IDbContext), typeof(BankContext));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ILancamentoServiceWrite), typeof(LancamentoService));
            services.AddScoped(typeof(ILancamentoAppService), typeof(LancamentoAppService));
        }

        public static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var builder = services.AddMvcCore();
            builder.AddJsonFormatters();
            builder.AddApiExplorer();
            builder.AddCors();

            return new MvcBuilder(builder.Services, builder.PartManager);
        }

        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            var builder = services.AddWebApi();
            builder.Services.Configure(setupAction);

            return builder;
        }

        public static void SwaggerUse(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/app/swagger.json", "Bank API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}