using mc.CodeAnalysis;
namespace mc.CodeAnalysis.Syntax
{
    internal sealed class Lexer

    {
        private readonly string _text;

        private SyntaxKind _kind;
        private int _start;
        private object? _value;

        // private List<string> _diagnostics = new List<string>();
        private DiagnosticBag _diagnostics = new DiagnosticBag();
        private int _position;

        public DiagnosticBag Diagnostics => _diagnostics;
        public Lexer(string text)
        {
            _text = text;
        }


        private void Next() { _position++; }

        private char Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _text.Length) return '\0';
            return _text[index];
        }
        private char Current => Peek(0);

        private char Lookahead => Peek(1);

        public SyntaxToken Lex()
        {

            _start = _position;
            _kind = SyntaxKind.BadToken;
            string? text = null;
            _value = null;
            switch (Current)
            {
                case '\0':
                    _kind = SyntaxKind.EndOfFileToken;
                    text = "\0";
                    break;
                case '+':
                    _position++;
                    _kind = SyntaxKind.PlusToken;
                    text = "+";
                    break;
                case '-':
                    _position++;
                    _kind = SyntaxKind.MinusToken;
                    text = "-";
                    break;
                case '*':
                    _position++;
                    _kind = SyntaxKind.StarToken;
                    text = "*";
                    break;

                case '/':
                    _position++;
                    _kind = SyntaxKind.SlashToken;
                    text = "/";
                    break;

                case '(':
                    _position++;
                    _kind = SyntaxKind.OpenParanthesisToken;
                    text = "(";
                    break;

                case ')':
                    _position++;
                    _kind = SyntaxKind.CloseParanthesisToken;
                    text = ")";
                    break;
                case '&':
                    if (Lookahead == '&')
                    {
                        _kind = SyntaxKind.AmpersandAmpersandToken;
                        _position += 2;
                        text = "&&";
                    }
                    else
                    {
                        _kind = SyntaxKind.AmpersandToken;
                        _position++;
                        text = "&";
                    }
                    break;
                case '^':
                    _position++;
                    _kind = SyntaxKind.CapToken;
                    text = "^";
                    break;
                case '|':
                    if (Lookahead == '|')
                    {
                        _kind = SyntaxKind.PipePipeToken;
                        _position += 2;
                        text = "||";
                    }
                    else
                    {
                        _kind = SyntaxKind.PipeToken;
                        _position++;
                        text = "|";
                    }
                    break;
                case '!':
                    if (Lookahead == '=')
                    {
                        _kind = SyntaxKind.BangEqualsToken;
                        _position += 2;
                        text = "!=";
                    }
                    else
                    {
                        _kind = SyntaxKind.BangToken;
                        _position++;
                        text = "!";
                    }
                    break;
                case '=':
                    if (Lookahead == '=')
                    {
                        _kind = SyntaxKind.EqualsEqualsToken;
                        _position += 2;
                        text = "==";
                    }
                    else
                    {
                        _kind = SyntaxKind.EqualToken;
                        text = "=";
                        _position++;
                    }
                    break;
                case '{':
                    _kind = SyntaxKind.OpenCurlyBrace;
                    _position++;
                    text = "{";
                    break;
                case '}':
                    _position++;
                    _kind = SyntaxKind.CloseCurlyBrace;
                    text = "}";
                    break;
                case '<':
                    if (Lookahead == '=')
                    {
                        _position += 2;
                        text = "<=";
                        _kind = SyntaxKind.LessOrEqualsToken;
                    }
                    else
                    {
                        _position++;
                        text = "<";
                        _kind = SyntaxKind.LessToken;
                    }
                    break;
                case '~':
                    _position++;
                    _kind = SyntaxKind.TildeToken;
                    text = "~";
                    break;
                case '>':
                    if (Lookahead == '=')
                    {
                        _position += 2;
                        text = ">=";
                        _kind = SyntaxKind.GreaterOrEqualsToken;
                    }
                    else
                    {
                        _position++;
                        text = ">";
                        _kind = SyntaxKind.GreaterToken;
                    }
                    break;
                default:
                    if (char.IsDigit(Current))
                    {
                        text = ReadNumber();
                    }
                    else if (char.IsWhiteSpace(Current))
                    {
                        text = ReadWhiteSpace();
                    }
                    else if (char.IsLetter(Current))
                    {
                        text = ReadLetter();
                    }
                    else
                    {
                        _diagnostics.ReportBadCharacter(_position, Current);
                        _position++;
                        text = _text.Substring(_position - 1, 1);
                        _value = null;
                    }
                    break;
            }


            return new SyntaxToken(_kind, _start, text, _value);
        }

        public string ReadNumber()
        {
            while (char.IsDigit(Current))
            {
                _position++;
            }
            var length = _position - _start;
            string text = _text.Substring(_start, length);
            if (!int.TryParse(text, out var value))
            {
                _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), text, typeof(int));
            }
            _value = value;
            _kind = SyntaxKind.NumberToken;
            return text;
        }
        public string ReadLetter()
        {

            while (char.IsLetter(Current))
            {
                _position++;
            }
            var length = _position - _start;
            string text = _text.Substring(_start, length);
            _kind = SyntaxFacts.GetKeywordKind(text);
            _value = SyntaxFacts.GetLetterValue(_kind);

            return text;

        }
        public string ReadWhiteSpace()
        {

            while (char.IsWhiteSpace(Current))
            {
                _position++;
            }
            var length = _position - _start;
            string text = _text.Substring(_start, length);
            _value = null;
            _kind = SyntaxKind.WhiteSpaceToken;
            return text;

        }
    }
}