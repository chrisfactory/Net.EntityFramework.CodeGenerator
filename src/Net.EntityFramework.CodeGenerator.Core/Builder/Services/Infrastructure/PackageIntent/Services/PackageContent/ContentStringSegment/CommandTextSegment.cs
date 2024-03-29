﻿using System.Diagnostics;
using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    [DebuggerDisplay("{_text}")]
    public class CommandTextSegment : IContentStringSegment
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
