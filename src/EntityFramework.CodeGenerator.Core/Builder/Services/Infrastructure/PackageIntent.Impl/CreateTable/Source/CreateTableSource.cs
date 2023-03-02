namespace EntityFramework.CodeGenerator.Core
{
    internal class CreateTableSource : ICreateSequenceSource
    {
        public CreateTableSource(IPackageScope scope)
        {
            Scope = scope;
        }
        public string Name { get; } = "Create Table";
        public IPackageScope Scope { get; }


    }
}
