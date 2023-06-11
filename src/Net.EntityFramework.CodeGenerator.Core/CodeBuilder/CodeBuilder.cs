using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public class CodeBuilder : ICodeBuilder
    {
        private int nbrIndent = 0;
        private string _indent = string.Empty;
        private StringBuilder _str = new StringBuilder();


        public ICodeBuilder AppendLine()
        {
            _str.AppendLine();
            return this;
        }
        public ICodeBuilder AppendLine(string? value)
        {
            _str.AppendLine($"{_indent}{value}");
            return this;
        }
        public ICodeBuilder Append(string? value)
        {
            _str.Append($"{value}");
            return this;
        }

        public string Build()
        {
            return _str.ToString();
        }

        public IDisposable Indent() => new IndentElement(this);
        private class IndentElement : IDisposable
        {
            private readonly CodeBuilder _builder;
            public IndentElement(CodeBuilder builder)
            {
                _builder = builder;
                lock (builder)
                    _builder.AddIndent();
            }
            public void Dispose()
            {
                lock (_builder)
                    _builder.RemoveIndent();
            }
        }

        private void AddIndent()
        {
            nbrIndent++;
            SetIndent();
        }

        private void RemoveIndent()
        {
            nbrIndent--;
            SetIndent();
        }
        private void SetIndent()
        {
            var s = string.Empty;
            for (int i = 0; i < nbrIndent; i++)
                s += "    ";
            _indent = s;
        }
    }
}
