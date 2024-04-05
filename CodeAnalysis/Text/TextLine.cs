namespace mc.CodeAnalysis.Text
{
    public sealed class TextLine
    {
        public string Text { get; }
        public int Length { get; }
        public int Start { get; }

        public int LineLengthWithBreak { get; }

        public int LastlineBreakWidth { get; }

        public TextLine(string text, int lineLength, int start, int lineLengthWithBreak, int lastlineBreakWidth)
        {
            Text = text;
            LineLengthWithBreak = lineLengthWithBreak;
            Length = lineLength;
            LastlineBreakWidth = lastlineBreakWidth;
            Start = start;
        }
    }
}