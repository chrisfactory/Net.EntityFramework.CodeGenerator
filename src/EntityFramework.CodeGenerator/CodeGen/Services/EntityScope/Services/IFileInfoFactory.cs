using System.IO;

namespace EntityFramework.CodeGenerator
{
    internal interface IFileInfoFactory
    {
        FileInfo CreateSqlFileInfo(string rootPath, string pattern, string? schema, string name, string? spName = null);

        FileInfo CreateCsFileInfo(string rootPath, string pattern, string? schema, string name, string classname);
    }
    internal class FileInfoFactory : IFileInfoFactory
    {
        public FileInfo CreateSqlFileInfo(string rootPath, string pattern, string? schema, string name, string? spName = null)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", string.IsNullOrWhiteSpace(schema) ? "" : schema);
            str.Add("{schemaExt}", string.IsNullOrWhiteSpace(schema) ? "" : $"{schema}.");
            str.Add("{name}", name);
            str.Add("{spname}", string.IsNullOrWhiteSpace(spName) ? "" : spName);
            var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }

        public FileInfo CreateCsFileInfo(string rootPath, string pattern, string? schema, string name, string classname)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", string.IsNullOrWhiteSpace(schema) ? "" : schema);
            str.Add("{name}", name);
            str.Add("{classname}", classname);
            var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }
    }
}
