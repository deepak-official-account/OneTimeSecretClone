using System.Collections.Generic;
using OneTimeSecretClone.Models;
using Microsoft.EntityFrameworkCore;

namespace OneTimeSecretClone
 {
    public class SecretDbContext : DbContext
    {
        //public SecretDbContext() { }
        public SecretDbContext(DbContextOptions<SecretDbContext> options) : base(options) { }
        public DbSet<SecretModel> Secrets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            }
    }
}
