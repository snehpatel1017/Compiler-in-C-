using mc.CodeAnalysis;
namespace mc.CodeAnalysis.Syntax
{
    public sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public LiteralExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value)
        {

        }
        public LiteralExpressionSyntax(SyntaxToken literalToken, object value)
        {
            LiteralToken = literalToken;
            Value = value;
        }
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken LiteralToken { get; }
        public object Value { get; }

        public override TextSpan Span => LiteralToken.Span;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }


    }
}