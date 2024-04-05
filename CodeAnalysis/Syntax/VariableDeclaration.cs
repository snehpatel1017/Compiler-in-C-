namespace mc.CodeAnalysis.Syntax
{
    public sealed class VariableDeclaration : StatementSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.VariableDeclarationStatement;

        public SyntaxToken Keyword { get; }
        public SyntaxToken Identifier { get; }
        public SyntaxToken EqualToken { get; }
        public ExpressionSyntax Initializer { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Keyword;
            yield return Identifier;
            yield return EqualToken;
            yield return Initializer;
        }

        public VariableDeclaration(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken equalToken, ExpressionSyntax initializer)
        {
            Keyword = keyword;
            Identifier = identifier;
            EqualToken = equalToken;
            Initializer = initializer;

        }
    }
}