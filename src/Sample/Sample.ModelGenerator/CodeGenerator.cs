using Microsoft.Extensions.DependencyInjection;
using SqlG;

namespace Sample.ModelGenerator
{
    partial class CodeGenerator
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.UseSqlG((p, b) =>
            {
                b.AddEntity();
            });
        }

    }
}