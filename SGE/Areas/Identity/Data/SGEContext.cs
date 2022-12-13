using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGE.Areas.Identity.Data;

namespace SGE.Data
{
    public class SGEContext : IdentityDbContext<IdentityUser>
    {
        public SGEContext(DbContextOptions<SGEContext> options)
            : base(options)
        {
        }
        public DbSet<EmpresaUsuario> EmpresaUsuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
