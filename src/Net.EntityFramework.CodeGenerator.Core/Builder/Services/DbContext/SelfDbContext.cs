using Microsoft.EntityFrameworkCore;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class SelfDbContext : DbContext
    {
        private readonly Action<ModelBuilder> _builder;
        public SelfDbContext(Action<ModelBuilder> builder, DbContextOptions<SelfDbContext> options) : base(options)
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
