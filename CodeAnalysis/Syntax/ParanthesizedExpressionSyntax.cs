namespace mc.CodeAnalysis.Syntax
{
    public sealed class ParanthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParanthesizedExpressionSyntax(SyntaxToken openParanthesis, ExpressionSyntax expression, SyntaxToken closeParanthesis)
        {
            OpenParanthisisToken = openParanthesis;
            Expression = expression;
            CloseParanthesisToken = closeParanthesis;
        }
        public override SyntaxKind Kind => SyntaxKind.ParanthesizedExpression;

        public SyntaxToken OpenParanthisisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParanthesisToken { get; }

        public override TextSpan Span => Expression.Span;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParanthisisToken;
            yield return Expression;
            yield return CloseParanthesisToken;

        }
    }
}