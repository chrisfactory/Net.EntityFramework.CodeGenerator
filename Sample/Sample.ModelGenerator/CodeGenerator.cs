using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using Sample.App;
using System.Security.Cryptography.X509Certificates;

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

                                var mapper = b.EntityMapper(); 
                                var select = b.SpSelect().Use(mapper);
                                //b.SpDelete();
                                //b.SpInsert();
                                //b.SpUpdate();
                                var x = b.DbService().Use(mapper, select);
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