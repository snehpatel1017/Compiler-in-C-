namespace Minsk.CodeAnalysis
{
    internal static class SyntaxFacts
    {
        public static int GetoperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 2;
                case SyntaxKind.MinusToken:
                case SyntaxKind.PlusToken:
                    return 1;
                default: return 0;
            }

        }
    }
}