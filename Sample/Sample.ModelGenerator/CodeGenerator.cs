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

                //var select = b.SpSelect();
                var select = b.SpSelectSingle();
                //var select = b.SpSelectSingleOrDefault();
                //var select = b.SpSelectFirst();
               // var select = b.SpSelectFirstOrDefault();

                b.EFDbService().Use(select);
            }).Property(typeof(string), "test");

            modelBuilder.Entity<Food2>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();

             //   var mapper = b.EntityMapper();
                //var select = b.SpSelect();

                //b.EFDbService().Use(select);
            });

            modelBuilder.Entity<FarmAnimal>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();
                 //var select = b.SpSelect();
             });
            modelBuilder.Entity<Cat>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();
                 //var select = b.SpSelect();
             });
            modelBuilder.Entity<Dog>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();
                 //var select = b.SpSelect();
             });
            modelBuilder.Entity<Human>()
             .GenerateFilesFor(b =>
             {
                 b.CreateTable();
                 b.CreateIndex();
                 //var select = b.SpSelect();
             });
            modelBuilder.Entity<CustomSchemaTableExemple>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();
                //var select = b.SpSelect();
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