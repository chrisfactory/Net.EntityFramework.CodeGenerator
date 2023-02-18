using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.App;
using SqlG;

namespace Sample.ModelGenerator
{


    partial class CodeGenerator
    {
        static void Main(string[] args)
        {


            var services = new ServiceCollection();
            services.AddSqlServerFileGenerator(
                        modelBuilder =>
                        {
                            modelBuilder.HasDefaultSqlTargetOutput(@"..\..\..\..\Sample.DbProj");
                            modelBuilder.HasDefaultCsTargetOutput(@"..\..\..\..\Sample.App");

                           // modelBuilder.HasDefaultSchema("dbo");

                            modelBuilder.Entity<Animal>()
                                 .UseTpcMappingStrategy()
                                 .Generate(b =>
                                 {

                                 });

                            modelBuilder.Entity<Food>().Property(typeof(string), "test");


                            modelBuilder.Entity<FarmAnimal>();
                            modelBuilder.Entity<Cat>();
                            modelBuilder.Entity<Dog>();
                            modelBuilder.Entity<Human>();

                         


                            modelBuilder.Generate(b =>
                            {
                                b.AllTables();
                                b.AllIndexs();
                                b.AllSequences();
                                b.AllSchema();
                            });
                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            Console.WriteLine("finished");
            //Console.ReadLine();
        }


    }

}