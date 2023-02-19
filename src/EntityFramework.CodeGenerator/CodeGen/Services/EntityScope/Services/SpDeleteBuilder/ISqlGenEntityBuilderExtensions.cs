using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder SpDelete(this ISqlGenEntityBuilder genBuilder, string name = null)
        {
            return genBuilder.AddGenActionBuilder<ISpDeleteBuilder, SpDeleteBuilder>(b =>
            {
                if (!string.IsNullOrEmpty(name))
                {
                    b.Services.AddSingleton<ISpNameProvider>(new FixedSpDeleteNameProvider(name));
                }
            });
        } 
    }
}
