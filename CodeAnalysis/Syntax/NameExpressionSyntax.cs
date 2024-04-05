using mc.CodeAnalysis.Syntax;
using mc.CodeAnalysis;

namespace mc.CodeAnalysis.Syntax
{
    public sealed class NameExpressionSyntax : ExpressionSyntax
    {
        public NameExpressionSyntax(SyntaxToken identifierToken)
        {
            IdentifierToken = identifierToken;
        }
        public SyntaxToken IdentifierToken { get; }
        public override SyntaxKind Kind => SyntaxKind.NameExpression;

        public override TextSpan Span => IdentifierToken.Span;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return IdentifierToken;

        }
    }


}