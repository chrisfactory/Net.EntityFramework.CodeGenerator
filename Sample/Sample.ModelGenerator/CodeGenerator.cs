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
                 .GenerateFilesFor(b =>
                 {
                     b.CreateTable();
                     b.CreateIndex();
                 });


            modelBuilder.Entity<Food>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();

                var s1 = b.SpSelect();
                var s2 = b.SpSelectSingle();
                var s3 = b.SpSelectSingleOrDefault();
                var s4 = b.SpSelectFirst();
                var s5 = b.SpSelectFirstOrDefault();

                b.EFDbService().Use(s1, s2, s3, s4, s5);
            }).Property(typeof(string), "test");

            modelBuilder.Entity<Food2>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();

                var s1 = b.SpSelect();
                var s2 = b.SpSelectSingle();
                var s3 = b.SpSelectSingleOrDefault();
                var s4 = b.SpSelectFirst();
                var s5 = b.SpSelectFirstOrDefault();

                b.EFDbService().Use(s1, s2, s3, s4, s5);
            });

            modelBuilder.Entity<FarmAnimal>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();

                 var s1 = b.SpSelect();
                 var s2 = b.SpSelectSingle();
                 var s3 = b.SpSelectSingleOrDefault();
                 var s4 = b.SpSelectFirst();
                 var s5 = b.SpSelectFirstOrDefault();

                 b.EFDbService().Use(s1, s2, s3, s4, s5);
             });
            modelBuilder.Entity<Cat>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();

                 var s1 = b.SpSelect();
                 var s2 = b.SpSelectSingle();
                 var s3 = b.SpSelectSingleOrDefault();
                 var s4 = b.SpSelectFirst();
                 var s5 = b.SpSelectFirstOrDefault();

                 b.EFDbService().Use(s1, s2, s3, s4, s5);
             });
            modelBuilder.Entity<Dog>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();

                 var s1 = b.SpSelect();
                 var s2 = b.SpSelectSingle();
                 var s3 = b.SpSelectSingleOrDefault();
                 var s4 = b.SpSelectFirst();
                 var s5 = b.SpSelectFirstOrDefault();

                 b.EFDbService().Use(s1, s2, s3, s4, s5);
             });
            modelBuilder.Entity<Human>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();

                 var s1 = b.SpSelect();
                 var s2 = b.SpSelectSingle();
                 var s3 = b.SpSelectSingleOrDefault();
                 var s4 = b.SpSelectFirst();
                 var s5 = b.SpSelectFirstOrDefault();

                 b.EFDbService().Use(s1, s2, s3, s4, s5);
             });
            modelBuilder.Entity<CustomSchemaTableExemple>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();

                var s1 = b.SpSelect();
                var s2 = b.SpSelectSingle();
                var s3 = b.SpSelectSingleOrDefault();
                var s4 = b.SpSelectFirst();
                var s5 = b.SpSelectFirstOrDefault();

                b.EFDbService().Use(s1, s2, s3, s4, s5);
            });


            modelBuilder
            .GenerateFilesFor(b =>
            {
                b.EnsureSchemas();
                b.CreateSequences();
            });
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