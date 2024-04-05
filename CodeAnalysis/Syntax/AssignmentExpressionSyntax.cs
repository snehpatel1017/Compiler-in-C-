using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis.Syntax
{
    public sealed class AssignmentExpressionSyntax : ExpressionSyntax
    {
        public AssignmentExpressionSyntax(SyntaxToken identfierToken, SyntaxToken equalToken, ExpressionSyntax expression)
        {
            IdentifierToken = identfierToken;
            EqualToken = equalToken;
            Expression = expression;
        }

        public SyntaxToken IdentifierToken { get; }
        public SyntaxToken EqualToken { get; }
        public ExpressionSyntax Expression { get; }
        public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;

        public override TextSpan Span => new TextSpan(IdentifierToken.Span.Start, Expression.Span.Start + Expression.Span.Length - 1 - IdentifierToken.Span.Start);

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return IdentifierToken;
            yield return EqualToken;
            yield return Expression;

        }
    }
}