using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;
using Sample.App;
using System.Security.Cryptography.X509Certificates;

namespace Sample.ModelGenerator
{

    partial class CodeGenerator
    {
        static int idx = 0;
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddSqlServerCodeGenerator(
                        modelBuilder =>
                        {

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


                                //var mapper = b.EntityMapper(); 
                                var select = b.SpSelect();//.Use(mapper);

                                //b.SpDelete();
                                //b.SpInsert();
                                //b.SpUpdate();
                                //var x = b.DbService().Use(mapper, select);
                            }).Property(typeof(string), "test");

                            modelBuilder.Entity<Food2>()
                            .GenerateFilesFor(b =>
                            {
                                b.CreateTable();
                                b.CreateIndex();
                                var select = b.SpSelect();
                            });

                            modelBuilder.Entity<FarmAnimal>()
                             .GenerateFilesFor(b =>
                             {
                                 b.CreateTable();
                                 b.CreateIndex();
                                 var select = b.SpSelect();
                             });
                            modelBuilder.Entity<Cat>()
                             .GenerateFilesFor(b =>
                             {
                                 b.CreateTable();
                                 b.CreateIndex();
                                 var select = b.SpSelect();
                             });
                            modelBuilder.Entity<Dog>()
                             .GenerateFilesFor(b =>
                             {
                                 b.CreateTable();
                                 b.CreateIndex();
                                 var select = b.SpSelect();
                             });
                            modelBuilder.Entity<Human>()
                             .GenerateFilesFor(b =>
                             {
                                 b.CreateTable();
                                 b.CreateIndex();
                                 var select = b.SpSelect();
                             });
                            modelBuilder.Entity<CustomSchemaTableExemple>()
                            .GenerateFilesFor(b =>
                            {
                                b.CreateTable();
                                b.CreateIndex();
                                var select = b.SpSelect();
                            });




                            modelBuilder
                            .GenerateFilesFor(b =>
                               {
                                   b.EnsureSchemas();
                                   b.CreateSequences();
                               });
                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            Console.WriteLine("finished");
            Console.ReadLine();
        }


    }

}