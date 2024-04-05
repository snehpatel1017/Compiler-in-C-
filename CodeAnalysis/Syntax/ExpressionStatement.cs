namespace mc.CodeAnalysis.Syntax
{
    public sealed class ExpressionStatement : StatementSyntax
    {

        public ExpressionStatement(ExpressionSyntax expression)
        {
            _expression = expression;
        }
        public ExpressionSyntax _expression { get; }
        public override SyntaxKind Kind => SyntaxKind.ExpressionStatement;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return _expression;
        }

    }
}
