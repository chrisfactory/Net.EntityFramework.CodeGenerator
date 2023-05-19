using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IPackageModuleBuilder : IBuilder<IModulePackage>
    {
        IPackageTokenProvider PackageTokenProvider { get; }
    }

    public static class IPackageModuleBuilderExtensions
    {
        public static IPackageToken UsePackageBuilder<TPackageBuilder, TPackageBuilderImplementation>(this IPackageModuleBuilder builder, Action<TPackageBuilder>? configure = null)
            where TPackageBuilder : class, IPackageBuilder
            where TPackageBuilderImplementation : class, TPackageBuilder
        {
            var token = builder.PackageTokenProvider.CreateToken();
            builder.Services.TryAddTransient<TPackageBuilder, TPackageBuilderImplementation>();

            builder.Services.AddSingleton<IPackageBuilderKey>(p =>
            {
                var builder = p.GetRequiredService<TPackageBuilder>();
                builder.Services.AddSingleton(token);
                configure?.Invoke(builder);
                return new PackageBuilderKey(token, builder);
            });

            return token;
        }

        private class PackageBuilderKey : IPackageBuilderKey
        {
            public PackageBuilderKey(IPackageToken token, IPackageBuilder builder)
            {
                Token = token;
                Builder = builder;
            }

            public IPackageToken Token { get; }
            public IPackageBuilder Builder { get; }
        }
    }
}
