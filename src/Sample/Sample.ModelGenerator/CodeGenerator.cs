using DataBaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class TargetSqlOutput
    {
        public const string AnnotationKey = $"sqlg.{nameof(TargetSqlOutput)}";

        public string TablesPattern { get; set; } = @"{schema}\Tables";
        public string StoredProceduresPattern { get; set; } = @"{schema}\Stored Procedures";
        public string IndexsPattern { get; set; } = @"{schema}\Indexs\{tableName}";
        public string SequencesPattern { get; set; } = @"{schema}\Sequences\{tableName}";
        public string SchemasPattern { get; set; } = @"Schemas";
    }

    partial class CodeGenerator
    {
        [STAThread]
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSqlServerFileGenerator(
                        modelBuilder =>
                        {
                            modelBuilder.HasAnnotation(TargetSqlOutput.AnnotationKey, new TargetSqlOutput());


                            modelBuilder.HasDefaultSchema("dbo");
                            modelBuilder.Entity<Animal>().UseTpcMappingStrategy();
                            modelBuilder.Entity<Food>();
                            modelBuilder.Entity<FarmAnimal>();
                            modelBuilder.Entity<Cat>();
                            modelBuilder.Entity<Dog>();
                            modelBuilder.Entity<Human>();
                            modelBuilder.HasSequence("CustomSequence", "customShema");

                        });


            var provider = services.BuildServiceProvider();
            var codeGen = provider.GetServices<ICodeGenerator>().ToList();


            if (codeGen != null)
            {

            }




            var optionsBuilder = new DbContextOptionsBuilder<CustomDbContext>();

            optionsBuilder.UseSqlServer(sql =>
            {

            });
            var f = new CustomDbContext(optionsBuilder.Options);

            var designTimeMode = f.GetService<IDesignTimeModel>();
            var model = designTimeMode.Model;
            var rm = model.GetRelationalModel();
            var diff = f.GetService<IMigrationsModelDiffer>();
            var cmds = diff.GetDifferences(null, rm);
            var mig = f.GetService<IMigrationsSqlGenerator>();
            // var mig = f.GetService<imigr>();
            int sequenceId = 1;
            foreach (var item in cmds)
            {

                if (item is Microsoft.EntityFrameworkCore.Migrations.Operations.CreateTableOperation co)
                {
                    var migCmds = mig.Generate(new List<MigrationOperation> { item });
                    foreach (var cmd in migCmds)
                    {
                        // File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Tables\{co.Schema}.{co.Name}.sqlg.sql", cmd.CommandText);
                    }
                    var entity = model.GetEntityTypes().Single(e => e.GetTableName() == co.Name);
                    var storedProcedure = entity.GetDeleteStoredProcedure();

                    if (storedProcedure != null)
                    {
                        var annotations = storedProcedure.GetAnnotations().ToList();


                        //var path = storedProcedure.GetAnnotation("sqlg.target.path");
                        //var fileName = storedProcedure.GetAnnotation("sqlg.target.filename");
                    }






                    AddInsert(co);
                    AddUpdate(co);
                    AddDelete(co);

                    AddDbAccess(co, model, entity);
                    AddMapExt(co, model, entity);
                }
                else if (item is Microsoft.EntityFrameworkCore.Migrations.Operations.EnsureSchemaOperation sch)
                {
                    var migCmds = mig.Generate(new List<MigrationOperation> { item });
                    foreach (var cmd in migCmds)
                    {
                        var t = item.GetType().FullName;
                        //File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\Schemas\{sch.Name}.sqlg.sql", cmd.CommandText);
                    }


                }
                else if (item is Microsoft.EntityFrameworkCore.Migrations.Operations.CreateIndexOperation idx)
                {
                    var migCmds = mig.Generate(new List<MigrationOperation> { item });
                    foreach (var cmd in migCmds)
                    {
                        // File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Indexs\{idx.Schema}.{idx.Name}.sqlg.sql", cmd.CommandText);
                    }
                }
                else if (item is Microsoft.EntityFrameworkCore.Migrations.Operations.CreateSequenceOperation seq)
                {
                    var migCmds = mig.Generate(new List<MigrationOperation> { item });
                    foreach (var cmd in migCmds)
                    {
                        // File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Sequence\{seq.Schema}.{seq.Name}.sqlg.sql", cmd.CommandText);
                        sequenceId++;
                    }
                }
                else
                {

                }
            }





            //var s = new ServiceCollection();


            //s.AddEntityFrameworkSqlServer();

            //s.AddDbContext<CustomDbContext>(o =>
            //{
            //    o.UseSqlServer();
            //});
            //var p = s.BuildServiceProvider();

            //using (var scope = p.CreateAsyncScope())
            //{
            //    var sP = scope.ServiceProvider;
            //    var mBuilder = sP.GetRequiredService<IMigrationsSqlGenerator>();
            //}


            //var services = new ServiceCollection();

            ////services.UseSqlQerverFileGenerator< CustomDbContext>((p, b) =>
            ////{
            ////    b.WithTargetModelProject("Sample.App");
            ////    b.WithTargetSqlProject("Sample.DbProj");
            ////});

            //var provider = services.BuildServiceProvider();
            //var generator = provider.GetService<SqlG.ICodeGenerator>();


            Console.ReadLine();
        }

        private static void AddInsert(CreateTableOperation co)
        {


            var includePK = co.PrimaryKey?.Columns.Length > 1;
            var buildColumns = co.Columns.ToList();
            if (!includePK && co.PrimaryKey != null)
                buildColumns.RemoveAll(c => co.PrimaryKey.Columns.Contains(c.Name));


            var sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [{co.Schema}].[Insert{co.Name}]");
            sb.AppendLine("(");

            var cCount = buildColumns.Count;
            var i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                sb.AppendLine($"     @{param.Name} {param.ColumnType?.ToUpper()}{end}");

            }
            sb.AppendLine(")");
            sb.AppendLine("AS BEGIN");
            sb.AppendLine("");
            sb.AppendLine($"    INSERT INTO [{co.Schema}].[{co.Name}]");
            sb.AppendLine("                 (");
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                sb.AppendLine($"                    [{param.Name}]{end}");

            }
            sb.AppendLine("                 )");
            sb.AppendLine("         VALUES");
            sb.AppendLine("                 (");
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                sb.AppendLine($"                    @{param.Name}{end}");

            }
            sb.AppendLine("                 )");
            sb.AppendLine("");
            sb.AppendLine("END");

            File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Programmability\{co.Schema}.Insert{co.Name}.sqlg.sql", sb.ToString());
        }

        private static void AddDelete(CreateTableOperation co)
        {
            if (co.PrimaryKey != null)
            {

                var sb = new StringBuilder();
                sb.AppendLine($"CREATE PROCEDURE [{co.Schema}].[Delete{co.Name}]");
                sb.AppendLine("(");
                var cCount = co.PrimaryKey.Columns.Length;
                var i = cCount;
                foreach (var param in co.PrimaryKey.Columns)
                {
                    i--;
                    var c = co.Columns.Single(c => c.Name == param);
                    string end = (i > 0) ? "," : "";
                    sb.AppendLine($"     @{param} {c.ColumnType?.ToUpper()}{end}");

                }
                sb.AppendLine(")");
                sb.AppendLine("AS BEGIN");
                sb.AppendLine("");
                sb.AppendLine($"    DELETE FROM [{co.Schema}].[{co.Name}]");
                sb.AppendLine($"    WHERE");
                i = cCount;
                foreach (var param in co.PrimaryKey.Columns)
                {
                    string and = cCount > 1 && i != cCount ? "AND " : (cCount > 1 ? "    " : string.Empty);
                    sb.AppendLine($"        {and}[{param}] = @{param}");
                    i--;
                }
                sb.AppendLine("");
                sb.AppendLine("END");

                File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Programmability\{co.Schema}.Delete{co.Name}.sqlg.sql", sb.ToString());
            }
        }
        private static void AddUpdate(CreateTableOperation co)
        {
            if (co.PrimaryKey == null)
                return;
            var buildColumns = co.Columns.ToList();

            buildColumns.RemoveAll(c => co.PrimaryKey.Columns.Contains(c.Name));

            if (buildColumns.Count == 0)
                return;

            var sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [{co.Schema}].[Update{co.Name}]");
            sb.AppendLine("(");
            var i = co.Columns.Count;
            foreach (var param in co.Columns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                sb.AppendLine($"     @{param.Name} {param.ColumnType?.ToUpper()}{end}");
            }
            sb.AppendLine(")");
            sb.AppendLine("AS BEGIN");
            sb.AppendLine("");
            sb.AppendLine($"    UPDATE [{co.Schema}].[{co.Name}]");
            sb.AppendLine($"    SET");


            if (co.PrimaryKey != null)
                buildColumns.RemoveAll(c => co.PrimaryKey.Columns.Contains(c.Name));
            var cCount = buildColumns.Count;
            i = cCount;
            foreach (var param in buildColumns)
            {
                i--;
                string end = (i > 0) ? "," : "";
                sb.AppendLine($"        {param.Name} = @{param.Name}{end}");
            }
            sb.AppendLine($"    WHERE");

            cCount = co.PrimaryKey.Columns.Length;
            i = cCount;
            foreach (var param in co.PrimaryKey.Columns)
            {
                string and = cCount > 1 && i != cCount ? "AND " : (cCount > 1 ? "    " : string.Empty);
                sb.AppendLine($"        {and}[{param}] = @{param}");
                i--;
            }
            sb.AppendLine("");
            sb.AppendLine("END");

            File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.DbProj\dbo\Programmability\{co.Schema}.Update{co.Name}.sqlg.sql", sb.ToString());

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


            File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.App\SqlG\{className}.sqlg.cs", sb.ToString());

        }

        private static void AddMapExt(CreateTableOperation co, IModel model, IEntityType entity)
        {
            string typeName = entity.ClrType.Name;
            string className = $"{typeName}MapExtensions";


            if (entity.ClrType == typeof(System.Collections.Generic.Dictionary<string, object>))
                return;
            var dictionaryProps = new Dictionary<string, PropertyInfo>();
            var runtimeProps = entity.GetRuntimeProperties().Values.ToList();
            foreach (var item in runtimeProps)
            {
                if (item != null)
                {
                    var attr = item.GetCustomAttribute<ColumnAttribute>();
                    if (attr != null && !string.IsNullOrEmpty(attr.Name))
                        dictionaryProps.Add(attr.Name, item);
                    else
                        dictionaryProps.Add(item.Name, item);
                }
            }


            var sb = new StringBuilder();
            sb.AppendLine($"using {typeof(IDataRecord).Namespace};");
            sb.AppendLine($"using {typeof(Tools).Namespace};");
            sb.AppendLine($"using {entity.ClrType.Namespace};");
            sb.AppendLine();
            sb.AppendLine($"public static partial class {className}");
            sb.AppendLine("{");




            sb.AppendLine($"    public static {typeName} Map(this {typeName} data, {nameof(IDataRecord)} dataRecord)");
            sb.AppendLine("    {");
            foreach (var item in co.Columns)
            {
                if (dictionaryProps.ContainsKey(item.Name))
                {
                    var clrProp = dictionaryProps[item.Name];
                    var comment = item.Name != clrProp.Name ? $"// => {item.Name}" : string.Empty;
                    sb.AppendLine($"        data.{clrProp.Name} = dataRecord.Get<{clrProp.PropertyType.ToCSharpString()}>(nameof({typeName}.{clrProp.Name}));{comment}");
                }
            }
            sb.AppendLine($"        return data;");
            sb.AppendLine("    }");


            sb.AppendLine("");



            sb.AppendLine($"    public static IReadOnlyDictionary<string, object?> Map(this {typeName} data)");
            sb.AppendLine("    {");
            sb.AppendLine("        var result = new Dictionary<string, object?>");
            sb.AppendLine("        {");
            var i = co.Columns.Count;
            foreach (var item in co.Columns)
            {
                i--;
                if (dictionaryProps.ContainsKey(item.Name))
                {
                    var clrProp = dictionaryProps[item.Name];
                    string end = (i > 0) ? "," : "";
                    sb.AppendLine($"            {{ \"@{item.Name}\", data.{clrProp.Name} }}{end}");
                }
            }
            sb.AppendLine("        };");
            sb.AppendLine($"        return result;");
            sb.AppendLine("    }");
            sb.AppendLine("}");



            File.WriteAllText($@"C:\Git\Chrisfactory\Lab\SqlG\src\Sample\Sample.App\SqlG\{className}.sqlg.cs", sb.ToString());

        }



    }

    public static class ToCSharpTypes
    {
        public static string ToCSharpString(this Type type)
        {
            if (type == null)
                return string.Empty;

            if (!type.IsGenericType)
            {
                if (type == typeof(System.Int32))
                    return "int";
                if (type == typeof(System.String))
                    return "string";
                if (type == typeof(System.Decimal))
                    return "decimal";
                if (type == typeof(System.Boolean))
                    return "bool";
                if (type == typeof(System.Byte))
                    return "byte";
                if (type == typeof(System.SByte))
                    return "sbyte";
                if (type == typeof(System.Char))
                    return "char";
                if (type == typeof(System.Double))
                    return "double";
                if (type == typeof(System.Single))
                    return "float";
                if (type == typeof(System.UInt32))
                    return "uint";
                if (type == typeof(System.IntPtr))
                    return "nint";
                if (type == typeof(System.UIntPtr))
                    return "nuint";
                if (type == typeof(System.Int64))
                    return "long";
                if (type == typeof(System.UInt64))
                    return "ulong";
                if (type == typeof(System.Int16))
                    return "short";
                if (type == typeof(System.UInt16))
                    return "ushort";
                if (type == typeof(System.Object))
                    return "object";

                return type.Name;
            }

            if (type == typeof(Nullable<System.Int32>))
                return "int?";
            if (type == typeof(Nullable<System.Decimal>))
                return "decimal?";
            if (type == typeof(Nullable<System.Boolean>))
                return "bool?";
            if (type == typeof(Nullable<System.Byte>))
                return "byte?";
            if (type == typeof(Nullable<System.SByte>))
                return "sbyte?";
            if (type == typeof(Nullable<System.Char>))
                return "char?";
            if (type == typeof(Nullable<System.Double>))
                return "double?";
            if (type == typeof(Nullable<System.Single>))
                return "float?";
            if (type == typeof(Nullable<System.UInt32>))
                return "uint?";
            if (type == typeof(Nullable<System.IntPtr>))
                return "nint?";
            if (type == typeof(Nullable<System.UIntPtr>))
                return "nuint?";
            if (type == typeof(Nullable<System.Int64>))
                return "long?";
            if (type == typeof(Nullable<System.UInt64>))
                return "ulong?";
            if (type == typeof(Nullable<System.Int16>))
                return "short?";
            if (type == typeof(Nullable<System.UInt16>))
                return "ushort?";

            return type.Name;
        }




    }


}