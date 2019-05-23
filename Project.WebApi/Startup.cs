using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Project.Core.User;
using Project.DAL;
using Project.IDAL;
using Project.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Project.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MyDbFactory.Setup(Configuration["MySqlConnectionString"], Configuration["SqlConnectionString"]);
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(ops => { ops.Filters.Add(new ValidateAttribute()); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();

            services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 50000000; });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "netCore API"
                });
                c.DescribeAllEnumsAsStrings();
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }
            );

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserContext, UserContext>();

            var builder = new ContainerBuilder();

            #region 跨域
            services.AddCors(options =>
                options.AddPolicy("any",
            o => o.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
);

            #endregion

            #region 手动配置（停用）

            //builder.Populate(services);
            //builder.RegisterType<ProductManage>().As<IProductManage>();
            //builder.RegisterType<ProductService>().As<IProductService>();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();

            #endregion

            #region 自动配置

            var serviceAssembly = Assembly.Load("Project.BLL");
            var repositoryAssembly = Assembly.Load("Project.DAL");
            var assemblies = new[] { serviceAssembly, repositoryAssembly };
            //自动注入所有服务
            builder.RegisterAssemblyTypes(assemblies)
                .Where(it =>
                    it.Name.EndsWith("Service", StringComparison.Ordinal) ||
                    it.Name.EndsWith("Manage", StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();
            //注册unitOfWork并允许其中使用属性注入
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();
            builder.Populate(services);

            #endregion

            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseCors("any");

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(new { ex.Message }.ToString(), Encoding.UTF8);
                    }
                    else
                    {
                        await context.Response.WriteAsync(new { Message = "Unknown error" }.ToString(), Encoding.UTF8);
                    }
                });
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "netCore API"); });

            app.UseMvc();

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}