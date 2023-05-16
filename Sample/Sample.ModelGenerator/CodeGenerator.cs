using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using Sample.App;

namespace Sample.ModelGenerator
{

    partial class CodeGenerator
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddSqlServerCodeGenerator(
                        modelBuilder =>
                        {


                            //modelBuilder.HasDefaultSqlTargetOutput(@"..\..\..\..\Sample.DbProj");
                            //modelBuilder.HasDefaultCsTargetOutput(@"..\..\..\..\Sample.App");

                            modelBuilder.HasDefaultSchema("dbo");

                            modelBuilder.Entity<Animal>()
                                 .UseTpcMappingStrategy();


                            modelBuilder.Entity<Food>()
                            .Generate(b =>
                            {
                                b.CreateIndex();
                                b.CreateTable();
                                var select = b.SpSelect();
                                //b.SpSelect();
                                //b.SpDelete();
                                //b.SpInsert();
                                //b.SpUpdate();
                                //b.DbService(select);
                            }).Property(typeof(string), "test");
                            modelBuilder.Entity<Food2>();

                            modelBuilder.Entity<FarmAnimal>();
                            modelBuilder.Entity<Cat>();
                            modelBuilder.Entity<Dog>();
                            modelBuilder.Entity<Human>();




                           // modelBuilder.Generate(b =>
                            //{
                            //    b.CreateSequences();
                            //    b.EnsureSchemas();
                            //});
                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            Console.WriteLine("finished");
            Console.ReadLine();
        }


    }

}