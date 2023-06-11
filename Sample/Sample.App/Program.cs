using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.ModelGenerator;
using System.Configuration;

namespace Sample.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //sqlpackage /Action:Publish /SourceFile:Sample.DbProj.dacpac /TargetConnectionString:"Server=.;Initial Catalog=CodeGenerator.Sample.Db;Integrated Security=SSPI;TrustServerCertificate=true" 


            var services = new ServiceCollection();
            services.AddDbContext<SampleDbContext>(options =>
                options.UseSqlServer("Server=.;Initial Catalog=CodeGenerator.Sample.Db;Integrated Security=SSPI;TrustServerCertificate=true"));
            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<SampleDbContext>())
                {
                    //var food = new Food()
                    //{
                    //    Name = "New Food Name"
                    //};
                    //food = db.InsertFood(food);

                    //food.Name = "Updated Food Name";
                    //food = db.UpdateSingleFoodById(food);

                    //food = db.GetSingleFoodById(food.Id);

                    //food = db.DeleteSingleFoodById(food.Id);
                }
            }
            Console.WriteLine("Hello, World!");
        }


    }
}