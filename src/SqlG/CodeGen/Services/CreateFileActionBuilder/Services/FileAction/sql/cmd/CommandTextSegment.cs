using System.Text;

namespace SqlG
{
    public class CommandTextSegment : IContentFileSegment
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
    }
}
