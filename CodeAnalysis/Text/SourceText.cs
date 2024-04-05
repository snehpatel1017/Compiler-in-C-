using System.Text;
using mc.CodeAnalysis.Text;
namespace mc.CodeAnalysis.Text
{
    public sealed class SourceText
    {
        public List<TextLine> _text = new List<TextLine>();
        private StringBuilder _Text = new StringBuilder();

        public string Text => _Text.ToString();

        public SourceText(string text)
        {
            int position = 0;
            int start = 0;
            int LastlineBreakWidth = 0;
            while (position < text.Length)
            {
                int lineBreakWidth = GetLineBreakWidth(text, position);
                if (lineBreakWidth == 0) position++;
                else
                {
                    int length = position - start;
                    string line = text.Substring(start, length);
                    _Text.Append(line);
                    _text.Add(new TextLine(line, length, start, length + lineBreakWidth, LastlineBreakWidth));
                    position += lineBreakWidth;
                    LastlineBreakWidth += lineBreakWidth;
                    start = position;
                }

            }
            if (start <= position)
            {
                int length = position - start;
                string line = text.Substring(start, length);
                _Text.Append(line);
                _text.Add(new TextLine(line, length, start, length, LastlineBreakWidth));
            }

        }

        public static int GetLineNumber(int position, List<TextLine> text)
        {

            int lower = 0;
            int upper = text.Count - 1;
            while (lower < upper)
            {
                int mid = lower + (upper - lower) / 2;
                if ((text[mid].Start - text[mid].LastlineBreakWidth <= position) && (position < (text[mid].Start - text[mid].LastlineBreakWidth + text[mid].Length))) return mid + 1;
                else if (text[mid].Start - text[mid].LastlineBreakWidth > position)
                {
                    upper = mid - 1;
                }
                else
                {
                    lower = mid + 1;
                }
            }
            return lower + 1;
        }


        private static int GetLineBreakWidth(string text, int position)
        {
            var c = text[position];
            var l = position + 1 >= text.Length ? '\0' : text[position + 1];

            if (c == '\r' && l == '\n')
                return 2;

            if (c == '\r' || c == '\n')
                return 1;

            return 0;
        }

    }
}