using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EntityFramework.CodeGenerator
{
    internal class SelfHostDbContext : DbContext
    {
        private readonly Action<ModelBuilder> _builder;
        public SelfHostDbContext(Action<ModelBuilder> builder, DbContextOptions<SelfHostDbContext> options) : base(options)
        {
            _builder = builder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _builder(modelBuilder);

        }
    }
}
