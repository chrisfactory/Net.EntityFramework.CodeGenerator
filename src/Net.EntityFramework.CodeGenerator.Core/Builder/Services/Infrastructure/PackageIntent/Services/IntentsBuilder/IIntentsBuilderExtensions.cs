using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public static class IIntentsBuilderExtensions
    {
        public static void DefineIntent<TTarget, TContentProvider>(this IIntentsBuilder builder)
            where TTarget : class, ITarget
            where TContentProvider : class, IIntentContentProvider
        {
            builder.Services.AddSingleton(p =>
            {
                var services = builder.SnapshotPoint.CreateBranch();
                services.AddSingleton<IIntent, Intent>();
                services.AddSingleton<ITarget, TTarget>();
                services.AddSingleton<IIntentContentProvider, TContentProvider>();
                services.AddSingleton(sp => sp.GetRequiredService<IIntentContentProvider>().Get());
                return services.BuildServiceProvider().GetRequiredService<IIntent>();
            });
        }
    }
}
