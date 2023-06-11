using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using Sample.App;

namespace Sample.ModelGenerator
{
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
                 .GenerateFilesFor(AllFunctionsForSample);

            modelBuilder.Entity<Cat>()
                .GenerateFilesFor(AllFunctionsForSample)
                .Property(typeof(string), "ShadowPropertyTest");

            modelBuilder.Entity<Dog>()
                .GenerateFilesFor(AllFunctionsForSample);

            modelBuilder.Entity<BasicEntity>()
                .GenerateFilesFor(AllFunctionsForSample).Property(typeof(int?), "ShadowPropertyTest"); ;

            modelBuilder.Entity<Food>()
                .GenerateFilesFor(AllFunctionsForSample);
            modelBuilder.Entity<MyAnimal>()
                .GenerateFilesFor(AllFunctionsForSample);



            modelBuilder
            .GenerateFilesFor(b =>
            {
                b.EnsureSchemas();
                b.CreateSequences();
            });
        }

        private static void AllFunctionsForSample<TEntity>(IEntityModuleBuilder<TEntity> b)
             where TEntity : class
        {
            b.CreateTable();
            b.CreateIndex();

            var features = new IPackageToken[]
            {
                b.SpSelect(),
                b.SpSelectSingle(),
                b.SpSelectSingleOrDefault(),
                b.SpSelectFirst(),
                b.SpSelectFirstOrDefault(),

                b.SpInsert(),

                b.SpUpdate(),
                b.SpUpdateSingle(),
                b.SpUpdateSingleOrDefault(),
                b.SpUpdateFirst(),
                b.SpUpdateFirstOrDefault(),


                b.SpDelete(),
                b.SpDeleteSingle(),
                b.SpDeleteSingleOrDefault(),
                b.SpDeleteFirst(),
                b.SpDeleteFirstOrDefault(),
            };
             
            b.DbContextExtensions().Use(features);
        }
    } 
}