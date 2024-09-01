using Common.Commands;
using Common.Queries;
using Common.Repositories;
using Common.Services;
using Domain.Models;
using Domain.ReadModels;
using EasyNetQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Persistence.DbContexts;
using Persistence.Repositories;
using Persistence.Services;
using QuestionApi.Store;

namespace QuestionApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen();

            AddAuthorization(services);
            AddWriteModel(services);
            AddReadModels(services);
            AddMessageBroker(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c => 
            {
                c.RouteTemplate = "Question/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/Question/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        private void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                op.Authority = Configuration["AuthOptions:Authority"];
                op.Audience = Configuration["AuthOptions:Audience"];
                op.RequireHttpsMetadata = false;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:questions", policy => policy.Requirements.Add(new HasScopeRequirement("read:questions")));
                options.AddPolicy("write:questions", policy => policy.Requirements.Add(new HasScopeRequirement("write:questions")));
            });


            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddTransient<IPublishService, RabbitMqPublishService>();
        }

        private void AddReadModels(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<QuestionReadModelDbContext>((sp, opt) =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("ReadModelPostgressConnection"));
                opt.EnableSensitiveDataLogging(true);
                opt.UseInternalServiceProvider(sp);
            }, ServiceLifetime.Singleton);

            services.AddSingleton(typeof(IReadOnlyRepository<QuestionReadModel>), typeof(ReadOnlyQuestionRepository));
            services.AddTransient(typeof(IQuestionQuery), typeof(QuestionQuery));
        }

        private void AddWriteModel(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<QuestionDbContext>((sp, opt) =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("PostgressConnection"));
                opt.EnableSensitiveDataLogging(true);
                opt.UseInternalServiceProvider(sp);
            }, ServiceLifetime.Singleton);

            services.AddSingleton(typeof(IRepository<Question>), typeof(QuestionRepository));
            services.AddSingleton(typeof(ICachedRepository<Question>), typeof(CachedQuestionRepository));
            services.AddTransient(typeof(QuestionDataStore));
            services.AddTransient(typeof(CreateQuestionCommandHandler));
            services.AddTransient(typeof(UpdateQuestionCommand));
            services.AddTransient(typeof(UpdateQuestionCommandHandler));
        }

        private void AddMessageBroker(IServiceCollection services)
        {
            var rabbitMQSettings = Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
            services.AddSingleton(rabbitMQSettings);

            var bus = RabbitHutch.CreateBus($"host={rabbitMQSettings.HostName}");
            services.AddSingleton<IBus>(bus);
        }
    }
}
