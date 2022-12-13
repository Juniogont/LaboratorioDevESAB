using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGE.Data;
using SGE.Models;

[assembly: HostingStartup(typeof(SGE.Areas.Identity.IdentityHostingStartup))]
namespace SGE.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SGEContext>(options =>
                    options.UseMySql(Constantes.connectionStringIdentity));
                //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<SGEContext>();
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SGEContext>();
            });
        }
    }
}