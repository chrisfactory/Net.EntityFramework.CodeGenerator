using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using Sample.App;

namespace Sample.ModelGenerator
{

    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDataProject(@"..\..\..\..\Sample.DbProj");
            modelBuilder.HasDotNetProject(@"..\..\..\..\Sample.App");

            modelBuilder.HasDefaultSchema("dbo");



            modelBuilder.Entity<Animal>()
                 .UseTpcMappingStrategy()
                 .GenerateFilesFor(AllFunctions);
             
            modelBuilder.Entity<Cat>()
                .GenerateFilesFor(AllFunctions)
                .Property(typeof(string), "ShadowPropertyTest");

            modelBuilder.Entity<Dog>()
                .GenerateFilesFor(AllFunctions);

            modelBuilder.Entity<BasicEntity>()
                .GenerateFilesFor(AllFunctions).Property(typeof(int?), "ShadowPropertyTest"); ;

            modelBuilder.Entity<Food>()
                .GenerateFilesFor(AllFunctions);
            modelBuilder.Entity<MyAnimal>()
                .GenerateFilesFor(AllFunctions);



            modelBuilder
            .GenerateFilesFor(b =>
            {
                b.EnsureSchemas();
                b.CreateSequences();
            });
        }

        private static void AllFunctions<TEntity>(IEntityModuleBuilder<TEntity> b)
             where TEntity : class
        {
            b.CreateTable();
            b.CreateIndex();

            var s1 = b.SpSelect();
            var s2 = b.SpSelectSingle();
            var s3 = b.SpSelectSingleOrDefault();
            var s4 = b.SpSelectFirst();
            var s5 = b.SpSelectFirstOrDefault();

            var i1 = b.SpInsert();

            var u1 = b.SpUpdate();

            b.DbContextExtensions().Use(s1, s2, s3, s4, s5, i1, u1);
        }
    }



    public partial class CodeGenerator
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddSqlServerCodeGenerator<SampleDbContext>(opt => opt.UseSqlServer());


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            Console.WriteLine("finished");
            Console.ReadLine();
        }

    }

}