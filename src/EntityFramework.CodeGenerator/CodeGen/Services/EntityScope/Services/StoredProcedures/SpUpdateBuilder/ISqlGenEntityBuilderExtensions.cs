using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder SpUpdate(this ISqlGenEntityBuilder genBuilder, string name = null)
        {
            return genBuilder.AddGenActionBuilder<ISpUpdateBuilder, SpUpdateBuilder>(b =>
            {
                if (!string.IsNullOrEmpty(name))
                {
                    b.Services.AddSingleton<ISpNameProvider>(new FixedSpUpdateNameProvider(name));
                }
            });
        }
    }
}