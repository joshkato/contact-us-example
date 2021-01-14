using System;
using System.IO;
using ContactUsExample.Configuration;
using ContactUsExample.Data;
using ContactUsExample.Services;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ContactUsExample
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<ILiteDatabase, LiteDatabase>(CreateLiteDatabaseInstance);
            services.AddSingleton<IContactUsService, ContactUsService>();
            services.AddSingleton<IContactUsMessageRepository, ContactUsMessagesRepository>();

            services.Configure<DatabaseConfig>(Configuration.GetSection(DatabaseConfig.SECTION_NAME));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private LiteDatabase CreateLiteDatabaseInstance(IServiceProvider services)
        {
            var dbConfig = services.GetService<IOptions<DatabaseConfig>>()?.Value;

            var dbPath = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
            var dbFile = Path.Combine(dbPath ?? ".", dbConfig?.FileName ?? "messages.db");

            return new LiteDatabase(dbFile);
        }
    }
}
