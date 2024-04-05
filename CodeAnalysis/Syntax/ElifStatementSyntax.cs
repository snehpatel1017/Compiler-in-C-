namespace mc.CodeAnalysis.Syntax
{
    public sealed class ElifStatementSyntax : StatementSyntax
    {
        public ElifStatementSyntax(SyntaxToken keyword, SyntaxToken openBracket, ExpressionSyntax conditionstatement,
                           SyntaxToken closeBracket, StatementSyntax thenstatement)
        {
            Keyword = keyword;
            OpenBracket = openBracket;
            Conditionstatement = conditionstatement;
            CloseBracket = closeBracket;
            Thenstatement = thenstatement;
        }
        public override SyntaxKind Kind => SyntaxKind.ElifStatement;

        public SyntaxToken Keyword { get; }
        public SyntaxToken OpenBracket { get; }
        public ExpressionSyntax Conditionstatement { get; }
        public SyntaxToken CloseBracket { get; }
        public StatementSyntax Thenstatement { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Keyword;
            yield return OpenBracket;
            yield return Conditionstatement;
            yield return CloseBracket;
            yield return Thenstatement;
        }
    }
}

