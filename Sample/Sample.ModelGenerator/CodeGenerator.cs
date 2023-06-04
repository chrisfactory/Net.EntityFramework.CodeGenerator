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

        //public  Food2? GetFood2(int id)
        //{

        //    return this.Set<Food2>()
        //        .FromSql($"[dbo].[SelectCustomFood2ByFoodId] @FoodId={id}")
        //        .AsEnumerable()
        //        .FirstOrDefault();
        //    // return db.Database.SqlQuery<Food2>("truc",);
        //    //using (var command = this.Database.GetDbConnection().CreateCommand())
        //    //{
        //    //    command.CommandText = "[dbo].[SelectCustomFood2ByFoodId]";
        //    //    command.CommandType = CommandType.StoredProcedure;
        //    //    command.Parameters.Add(new SqlParameter("FoodId",id));

        //    //    command.Connection?.Open();
        //    //   // db.Database.OpenConnection();

        //    //    using (var result = command.ExecuteReader())
        //    //    {

        //    //        return ((IObjectContextAdapter)this).ObjectContext.Translate<Food2>(reader).Single();
        //    //    }
        //    //}

        //   // return ((IObjectContextAdapter)context).ObjectContext.Translate<Food2>(reader).Single();
        //}

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

              //  var mapper = b.EntityMapper();
                var select = b.SpSelectSingle();

                //b.SpDelete();
                //b.SpInsert();
                //b.SpUpdate();
                //b.DbService().Use(select);
                b.EFDbService().Use(select);
            }).Property(typeof(string), "test");

            modelBuilder.Entity<Food2>()
            .GenerateFilesFor(b =>
            {
                b.CreateTable();
                b.CreateIndex();

             //   var mapper = b.EntityMapper();
                var select = b.SpSelect();

                b.EFDbService().Use(select);
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