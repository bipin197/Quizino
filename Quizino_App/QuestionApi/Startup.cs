using Common.Commands;
using Common.Loaders;
using Common.Queries;
using Common.Repositories;
using Domain.Models;
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
                options.AddPolicy("read:questions", policy => policy.Requirements.Add(new HasScopeRequirement("read:questions")));
            });
            services.AddEntityFrameworkNpgsql().AddDbContext<QuestionDbContext>((sp, opt) =>
            {
                var conn = Configuration.GetConnectionString("PostgressConnection");
                opt.UseNpgsql(Configuration.GetConnectionString("PostgressConnection"));
                opt.EnableSensitiveDataLogging();
                opt.UseInternalServiceProvider(sp);
            });
            services.AddScoped(typeof(IRepository<Question>), typeof(JsonRepository<Question>));
            services.AddScoped(typeof(IRepository<Quiz>), typeof(JsonRepository<Quiz>));
            services.AddTransient(typeof(IQuestionQuery), typeof(QuestionQuery));
            services.AddTransient(typeof(IQuizQuery), typeof(QuizQuery));
            services.AddTransient(typeof(QuestionDataStore));
            services.AddTransient(typeof(CreateQuestionCommand));
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
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
            app.UseCors(options => options.WithOrigins("https://localhost:44349", "http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
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
    }
}
