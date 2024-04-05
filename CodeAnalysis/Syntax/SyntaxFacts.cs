namespace mc.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {
        public static int GetBinaryoperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 8;
                case SyntaxKind.MinusToken:
                case SyntaxKind.PlusToken:
                    return 7;
                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEqualsToken:
                case SyntaxKind.LessToken:
                case SyntaxKind.LessOrEqualsToken:
                case SyntaxKind.GreaterToken:
                case SyntaxKind.GreaterOrEqualsToken:
                    return 6;
                case SyntaxKind.AmpersandToken:
                    return 5;
                case SyntaxKind.CapToken:
                    return 4;
                case SyntaxKind.PipeToken:
                    return 3;
                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;
                case SyntaxKind.PipePipeToken:
                    return 1;
                default: return 0;
            }

        }
        public static int GetUnaryoperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {

                case SyntaxKind.BangToken:
                case SyntaxKind.TildeToken:
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 9;
                default: return 0;
            }

        }
        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "\0": return SyntaxKind.EndOfFileToken;
                case "false": return SyntaxKind.FalseKeyword;
                case "true": return SyntaxKind.TrueKeyword;
                case "var": return SyntaxKind.VarKeyword;
                case "const": return SyntaxKind.ConstKeyword;
                case "if": return SyntaxKind.IfKeyword;
                case "elif": return SyntaxKind.ElifKeyword;
                case "else": return SyntaxKind.ElseKeyword;
                case "while": return SyntaxKind.WhileKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }

        public static object? GetLetterValue(SyntaxKind kk)
        {
            switch (kk)
            {
                case SyntaxKind.FalseKeyword:
                    return false;
                case SyntaxKind.TrueKeyword:
                    return true;
                default:
                    return null;
            }
        }
    }
}