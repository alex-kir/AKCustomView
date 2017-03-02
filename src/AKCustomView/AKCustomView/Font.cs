using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK
{
    [Flags]
    public enum FontStyle
    {
        Regular = 0,
        Bold = 1,
        Italic = 2,
        //Underline = 4,
        //Strikeout = 8,
    }

    public class Font
    {
        public bool Bold { get { return Style.HasFlag(FontStyle.Bold); } }
        public FontStyle Style { get; private set; }

        public string Name { get { return OriginalFontName; } }
        public string OriginalFontName { get; private set; }
        public float Size { get; private set; }

        public Font(Font prototype, FontStyle newStyle) :this(prototype.OriginalFontName, prototype.Size, newStyle)
        { }

        public Font(string familyName, float emSize) : this(familyName, emSize, FontStyle.Regular)
        { }

        public Font(string familyName, float emSize, FontStyle style)
        {
            OriginalFontName = familyName;
            Size = emSize;
            Style = style;
        }
    }
}
