using System;
using System.Collections.Generic;
using System.Linq;

namespace BadLibs
{
    public struct TextSection
    {
        public string Text { get; set; }
        public bool Format { get; set; }

        public TextSection(string text)
        {
            Text = text;
            Format = false;
        }
    }
}
