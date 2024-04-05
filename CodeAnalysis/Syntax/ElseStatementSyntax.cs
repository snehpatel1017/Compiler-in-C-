namespace mc.CodeAnalysis.Syntax
{
    public sealed class ElseStatementSyntax : StatementSyntax
    {

        public ElseStatementSyntax(SyntaxToken keyword, StatementSyntax thenstatement)
        {
            Keyword = keyword;
            Thenstatement = thenstatement;
        }

        public override SyntaxKind Kind => SyntaxKind.ElseStatement;

        public SyntaxToken Keyword { get; }
        public StatementSyntax Thenstatement { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Keyword;
            yield return Thenstatement;
        }
    }
}

