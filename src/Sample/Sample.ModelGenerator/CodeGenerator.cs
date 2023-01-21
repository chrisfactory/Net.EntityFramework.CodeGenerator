using DotNetProjectParser;
using Microsoft.Extensions.DependencyInjection;
using Sample.App;
using Sample.App.NewFolder;
using SqlG;

namespace Sample.ModelGenerator
{
    partial class CodeGenerator
    {
        [STAThread]
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            
            services.UseSqlGContext((p, b) =>
            {
                b.WithTargetModelProject("Sample.App");
                b.WithTargetSqlProject("Sample.DbProj");
                b.AddEntity<UserSampleEntityModel>(ConfigureUserSampleEntityModel);
                b.AddEntity<EmailSampleEntityModel>(ConfigureEmailSampleEntityModel);
            });

            var provider = services.BuildServiceProvider();
            var generator = provider.GetService<SqlG.ICodeGenerator>();


            Console.ReadLine();
        }


        private static void ConfigureUserSampleEntityModel(IEntityStrategyBuilder strategy)
        { 
            strategy.UseEntityMapClassEntensions();
            strategy.UseEntityMapClassPartial();
        }
        private static void ConfigureEmailSampleEntityModel(IEntityStrategyBuilder strategy)
        {
            strategy.UseEntityMapClassEntensions();
            strategy.UseEntityMapClassPartial();
        }


    }




}