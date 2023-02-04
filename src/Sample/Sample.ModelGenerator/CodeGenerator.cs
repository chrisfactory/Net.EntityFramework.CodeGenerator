using DataBaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Sample.App;
using SqlG;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

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
                            modelBuilder.Entity<Animal>().UseTpcMappingStrategy();
                            modelBuilder.Entity<Food>();
                            modelBuilder.Entity<FarmAnimal>();
                            modelBuilder.Entity<Cat>();
                            modelBuilder.Entity<Dog>();
                            modelBuilder.Entity<Human>();
                            //modelBuilder.HasSequence("CustomSequence", "customShema");

                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


   
            Console.ReadLine();
        }

 
        private static void AddDbAccess(CreateTableOperation co, IModel model, IEntityType entity)
        {
            string typeName = entity.ClrType.Name;
            string className = $"{typeName}DbService";

            if (entity.ClrType == typeof(System.Collections.Generic.Dictionary<string, object>))
                return;



            var sb = new StringBuilder();
            sb.AppendLine($"using {typeof(DBAccessBase).Namespace};");
            sb.AppendLine();
            sb.AppendLine($"internal partial class {className} : {nameof(DBAccessBase)}//, I{className}");
            sb.AppendLine("{");
            sb.AppendLine($"    public {className}(string cnx) : base(cnx)");
            sb.AppendLine("    { }");
            sb.AppendLine();




            sb.AppendLine();
            sb.AppendLine("}");

             

        }

  


    }

  


}