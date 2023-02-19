using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.App;
using EntityFramework.CodeGenerator;

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

                            modelBuilder.HasDefaultSchema("dbo");

                            modelBuilder.Entity<Animal>()
                                 .UseTpcMappingStrategy()
                                 .Generate(b =>
                                 {
                                     b.SpSelect();
                                     b.SpDelete();
                                     b.SpInsert();
                                     b.SpUpdate();
                                 });

                            modelBuilder.Entity<Food>().Property(typeof(string), "test");


                            modelBuilder.Entity<FarmAnimal>();
                            modelBuilder.Entity<Cat>();
                            modelBuilder.Entity<Dog>()
                            .Generate(b =>
                            {
                                b.SpSelect();
                                b.SpDelete();
                                b.SpInsert();
                                b.SpUpdate();
                            }); ;
                            modelBuilder.Entity<Human>();




                            modelBuilder.Generate(b =>
                            {
                                b.Tables();
                                b.Indexs();
                                b.Sequences();
                                b.Schemas();
                            });
                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            Console.WriteLine("finished");
            //Console.ReadLine();
        }


    }

}