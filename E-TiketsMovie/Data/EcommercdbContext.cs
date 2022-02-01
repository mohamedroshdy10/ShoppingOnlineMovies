using E_TiketsMovie.Models.Tables;
using E_TiketsMovie.Models.Tables.Peopel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Data
{
    public class EcommercdbContext: IdentityDbContext<AppliationUser>
    {
        public EcommercdbContext(DbContextOptions<EcommercdbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppliationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor_Movies>  Actor_Movies { get; set; }
        public DbSet<Produsser>  Produssers { get; set; }
        public DbSet<Movie>  Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderIteam>  OrderIteams { get; set; }
        public DbSet<ShoppingCardItems>  ShoppingCardItems { get; set; }
    }
}
