using Net.EntityFramework.CodeGenerator;
using Net.EntityFramework.CodeGenerator.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IDbServiceModuleIntentBuilderExtensions
    {
        public static IDbServiceModuleIntentBuilder CorrelateWith(this IDbServiceModuleIntentBuilder builder, IPackageToken[] with)
        {
            if(with == null || with.Length == 0) 
                return builder;
            builder.Services.AddSingleton<ITokenLink>(new TokenLink(with)); 
            return builder;
        }
    }
}
