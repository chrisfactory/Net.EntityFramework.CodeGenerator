using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder SpSelect(this ISqlGenEntityBuilder genBuilder, string? name = null)
        {
            return genBuilder.AddGenActionBuilder<ISpSelectBuilder, SpSelectBuilder>(b =>
            {
                if(!string.IsNullOrEmpty(name))
                {
                    b.Services.AddSingleton<ISpNameProvider>(new FixedSpSelectNameProvider(name));
                }
            });
        }
    }
}
