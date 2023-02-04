using System.Collections;
using System.Text;

namespace SqlG
{
    public class CommandTextSegment : IContentFileRootSegment
    {
        private readonly string _text;
        public CommandTextSegment(string text)
        {
            _text = text;
        }
        public void Build(StringBuilder builder)
        {
            builder.Append(_text);
        }

        public IEnumerator<IContentFileSegment> GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<IContentFileSegment>().GetEnumerator();
        }
    }
}
