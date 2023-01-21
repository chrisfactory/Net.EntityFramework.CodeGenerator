namespace SqlG
{
    internal class CodeGenerator : ICodeGenerator
    {
        public CodeGenerator(IEnumerable<IEntityStrategy> strategies , IProjectResolver s)
        {
            var st = strategies.ToList();
        }
    }
}
