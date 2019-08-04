using System;
using MassTransit;
using MassTransitSample.ProductApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MassTransitSample.ProductApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.Configure<RabbitMqConnectionConfig>(Configuration.GetSection("RabbitMqConnection"));
            
            var rabbitMqConnectionSection = Configuration.GetSection("RabbitMqConnection");
            
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri($"{rabbitMqConnectionSection.GetValue<string>("HostUrl")}"), h =>
                {
                    h.Username(rabbitMqConnectionSection.GetValue<string>("Username"));
                    h.Password(rabbitMqConnectionSection.GetValue<string>("Password"));
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Product API"
                });
            });

            services.AddSingleton<IPublishEndpoint>(bus);
            services.AddSingleton<ISendEndpointProvider>(bus);
            services.AddSingleton<IBus>(bus);
            
            bus.Start();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvcWithDefaultRoute();
            app.UseSwagger()
                         .UseSwaggerUI(c =>
                         {
                             c.SwaggerEndpoint(
                                 $"/swagger/v1/swagger.json",
                                 "Product.API V1");
                         });

        }
    }
}