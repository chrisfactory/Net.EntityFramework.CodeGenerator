namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface ICodeBuilder
    {
        ICodeBuilder AppendLine();
        ICodeBuilder AppendLine(string? value);

        IDisposable Indent();

        string Build();
    }
}
