using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;
using Tournament.Core;
using Tournament.Core.Repositories;
using Tournament.Core.Repository;
using Tournament.Infrastructure;
using Tournament.Infrastructure.Repositories;

namespace Tourament.Web
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

            services.AddMvc();
            services.AddHttpsRedirection(options => { options.HttpsPort = 443; });
            services.AddAutoMapper(typeof(Startup));
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddDbContext<TournamentContext>(options => options.UseNpgsql(Configuration["DefaultConnection"]));
            services.AddHangfire(x => x.UsePostgreSqlStorage(Configuration["DefaultConnection"]));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IBackgroundJobClient, BackgroundJobClient>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();
            services
                .AddFluentEmail("okulmef@gmail.com")
                .AddSmtpSender(new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential
                    {
                        UserName = "okulmef@gmail.com",
                        Password = "12qwerty34"
                        
                    },
                    EnableSsl = true
                }) ; 
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseForwardedHeaders();
                //app.UseHsts();
               
            }
            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,
            SslRedirect = false,
            LoginCaseSensitive = true,
            Users = new []
            {
                new BasicAuthAuthorizationUser
                {
                    Login = "admin",
                    PasswordClear =  "password"
                }
            }

        }) }
            });
            app.UseHangfireDashboard(); //Will be available under http://localhost:5000/hangfire"
            var options = new BackgroundJobServerOptions
            {
                WorkerCount = 1    //Hangfire's default worker count is 20, which opens 20 connections simultaneously.
                                   // For this we are overriding the default value.
            };

            app.UseHangfireServer(options);

        }
    }
}
