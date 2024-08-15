using Common.Queries;
using Common.Quiz.Queries;
using Common.Quiz.Repositories;
using Domain.Quiz.Interfaces;
using Domain.Quiz.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistent.Quiz.DbContexts;
using Persistent.Quiz.Repositories;

namespace QuizApi
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                op.Authority = "https://dev-duimink2n4isdefw.us.auth0.com/";
                op.Audience = "quizion-test-2";
                op.RequireHttpsMetadata = false;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:quizes", policy => policy.Requirements.Add(new HasScopeRequirement("read:quizes")));
                options.AddPolicy("write:quizes", policy => policy.Requirements.Add(new HasScopeRequirement("write:quizes")));
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<QuizWriteModelDbContext>((sp, opt) =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("PostgressConnection"));
                opt.EnableSensitiveDataLogging(true);
                opt.UseInternalServiceProvider(sp);
            }, ServiceLifetime.Singleton);

            services.AddEntityFrameworkNpgsql().AddDbContext<QuizReadModelDbContext>((sp, opt) =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("PostgressConnection"));
                opt.EnableSensitiveDataLogging(true);
                opt.UseInternalServiceProvider(sp);
            }, ServiceLifetime.Singleton);


            services.AddTransient(typeof(IRepository<QuizWriteModel>), typeof(CachedQuizRepository));
            services.AddTransient(typeof(IReadOnlyRepository<QuizReadModel>), typeof(CachedQuizReadOnlyRepository));
            services.AddTransient(typeof(IQuizQuery), typeof(QuizQuery));
            services.AddTransient(typeof(QuizWriteModel));
            services.AddTransient(typeof(QuizReadModel));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI();
                
            }

           // app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "Quiz/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/Quiz/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
