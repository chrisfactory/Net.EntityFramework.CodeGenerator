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
                    var foo = db.SelectCustomFood2ByFoodId(1);
                    //Get(db, 1).GetAwaiter().GetResult();
                    //  var fooAsync = db.SelectCustomFood2ByFoodIdAsync(1).GetAwaiter().GetResult();

                }
            }
            Console.WriteLine("Hello, World!");
        }

     
    }
}