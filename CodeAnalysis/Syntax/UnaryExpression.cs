namespace mc.CodeAnalysis.Syntax
{
    public sealed class UniaryExpressionSyntax : ExpressionSyntax
    {
        public UniaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax right)
        {

            OperatorToken = operatorToken;
            Expression = right;
        }
        public override SyntaxKind Kind => SyntaxKind.UniaryExpression;


        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Expression { get; }

        public override TextSpan Span => new TextSpan(OperatorToken.Span.Start, Expression.Span.Start + Expression.Span.Length - 1 - OperatorToken.Span.Start);

        public override IEnumerable<SyntaxNode> GetChildren()
        {

            yield return OperatorToken;
            yield return Expression;
        }
    }


}