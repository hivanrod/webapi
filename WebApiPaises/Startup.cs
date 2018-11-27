using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiPaises.Models;

namespace WebApiPaises
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
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            //   options.UseInMemoryDatabase("paisDB"));

            // agregado para aplica
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "yourdomain.com",
                        ValidAudience = "yourdomain.com",
                        IssuerSigningKey = new SymmetricSecurityKey(
                           // Encoding.UTF8.GetBytes(Configuration["llave_super"])),
                        Encoding.UTF8.GetBytes("GHJKYDKGYHJGKJGKYKGCDRYCTRYTYÑVIÑBDRRTSDBGI")),
                        ClockSkew = TimeSpan.Zero
                    });

            services.AddMvc().AddJsonOptions(ConfigureJson);
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

            //if (!context.Paises.Any())
            //{
            //    context.Paises.AddRange(new List<Pais>()
            //    {
            //        new Pais()
            //        {
            //            Nombre = "Colombia1",
            //            Provincias = new List<Provincia>() {
            //                new Provincia()
            //                {
            //                    Nombre ="Usme"
            //                }
            //            }
            //        },
            //        new Pais()
            //        {
            //            Nombre = "Usa3",
            //            Provincias = new List<Provincia>() {
            //                new Provincia()
            //                {
            //                    Nombre ="Puebla"
            //                },
            //                new Provincia()
            //                {
            //                    Nombre ="Queretaro"
            //                }
            //            }
            //        },
            //        new Pais()
            //        {
            //            Nombre = "Vene4"
            //        }
            //    });
            //    context.SaveChanges();
            //}


        }
    }
}
