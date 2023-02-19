using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder SpInsert(this ISqlGenEntityBuilder genBuilder, string name = null)
        {
            return genBuilder.AddGenActionBuilder<ISpInsertBuilder, SpInsertBuilder>(b =>
            {
                if (!string.IsNullOrEmpty(name))
                {
                    b.Services.AddSingleton<ISpNameProvider>(new FixedSpInsertNameProvider(name));
                }
            });
        } 
    }
}
