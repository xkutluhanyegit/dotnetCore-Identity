using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using rentid.Contexts;
using rentid.Entities;

namespace rentid
{
    public class Startup
    {
        private readonly IConfiguration config;
        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<appDbContext>(opt => {
                opt.UseSqlServer(config.GetConnectionString("SQL"));
            });

            services.AddIdentity<appUser,appRole>( opt => {
                //Sifre gereklilikleri
                opt.Password.RequireDigit=false;
                opt.Password.RequiredLength=6;
                opt.Password.RequireUppercase=false;
                opt.Password.RequireLowercase=false;
                opt.Password.RequireNonAlphanumeric=false;

            }).AddEntityFrameworkStores<appDbContext>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=anasayfa}/{action=index}/{id?}"
                );
            });
        }
    }
}
