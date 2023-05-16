using Microsoft.Extensions.DependencyInjection;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    public interface IPackageBuilder : IBuilder<IPackage>
    {
    }
    public interface IIntentsBuilder : IBuilder<IEnumerable<IIntent>>
    {
        INodeSnapshotPoint SnapshotPoint { get; }
    }

    public static class IIntentsBuilderExtensions
    {
        public static void DefineIntent<TTarget, TContentProvider>(this IIntentsBuilder builder)
            where TTarget : class, ITarget
            where TContentProvider : class, IIntentContentProvider
        {
            builder.Services.AddSingleton(p =>
            {
                IServiceCollection b1 = builder.SnapshotPoint.CreateBranch();
                b1.AddSingleton<IIntent, Intent>();
                b1.AddSingleton<ITarget, TTarget>();
                b1.AddSingleton<IIntentContentProvider, TContentProvider>();
                b1.AddSingleton(sp => sp.GetRequiredService<IIntentContentProvider>().Get());
                return b1.BuildServiceProvider().GetRequiredService<IIntent>();
            });
        }
    } 
           
    public abstract class PackageBuilder<TSource, TSourceImplementation> : IPackageBuilder
        where TSource : class, IPackageSource
        where TSourceImplementation : class, TSource
    {
        public PackageBuilder(IPackageStack packageStack)
        {
            Services = packageStack.GetNewStack<TSource, TSourceImplementation>();
        }
        public IServiceCollection Services { get; }

        protected abstract void DefineIntents(IIntentsBuilder intentBuilder);
        public IPackage Build()
        {
            var intentNode = Services.CreateNode();

            var internalPackageServices = intentNode.CreateBranch();

            internalPackageServices.AddSingleton<IPostBuildPackage, PackageModuleIntent>();
            internalPackageServices.AddSingleton<IPackage>(p => p.GetRequiredService<IPostBuildPackage>());
            internalPackageServices.AddSingleton(p =>
            {
                var builder = new IntentsBuilder(intentNode);
                DefineIntents(builder);
                return builder.Build();
            });
            return internalPackageServices.BuildServiceProvider().GetRequiredService<IPackage>();
        }

        private class IntentsBuilder : IIntentsBuilder
        {
            public IntentsBuilder(INodeSnapshotPoint node)
            {
                SnapshotPoint = node;
                Services = node.CreateBranch();
            }
            public IServiceCollection Services { get; }

            public INodeSnapshotPoint SnapshotPoint { get; }

            public IEnumerable<IIntent> Build()
            {
                return Services.BuildServiceProvider().GetRequiredService<IEnumerable<IIntent>>();
            }
        }
    }

    public class SpSelectPackageBuilder : PackageBuilder<ISpSelectCodeGeneratorSource, SpSelectSource>, ISpSelectPackageBuilder
    {
        public SpSelectPackageBuilder(IPackageStack packageStack)
            : base(packageStack)
        {
            Services.AddSingleton<IStoredProcedureSchemaProvider, TablePackageProcedureSchemaProvider>();
            Services.AddSingleton<IStoredProcedureNameProvider, SelectTableProcedureNameProvider>();
        }

        protected override void DefineIntents(IIntentsBuilder intentBuilder)
        {
            intentBuilder.DefineIntent<SqlSpSelectTarget, SqlSpSelectPackageContentProvider>();
            intentBuilder.DefineIntent<DbServiceSpSelectTarget, DbServiceSpSelectPackageContentProvider>();
        }
    }
}
