namespace mc.CodeAnalysis.Syntax
{
    public sealed class IfStatementSyntax : StatementSyntax
    {
        public IfStatementSyntax(SyntaxToken keyword, SyntaxToken openBracket, ExpressionSyntax conditionstatement,
                           SyntaxToken closeBracket, StatementSyntax thenstatement, List<ElifStatementSyntax> elseifblocks, ElseStatementSyntax? elseblock)
        {
            Keyword = keyword;
            OpenBracket = openBracket;
            Conditionstatement = conditionstatement;
            CloseBracket = closeBracket;
            Thenstatement = thenstatement;
            Elseifblocks = elseifblocks;
            Elseblock = elseblock;
        }

        public override SyntaxKind Kind => SyntaxKind.IfStatement;

        public SyntaxToken Keyword { get; }
        public SyntaxToken OpenBracket { get; }
        public ExpressionSyntax Conditionstatement { get; }
        public SyntaxToken CloseBracket { get; }
        public StatementSyntax Thenstatement { get; }
        public List<ElifStatementSyntax> Elseifblocks { get; }
        public ElseStatementSyntax? Elseblock { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Keyword;
            yield return OpenBracket;
            yield return Conditionstatement;
            yield return CloseBracket;
            yield return Thenstatement;
            foreach (var elseifblock in Elseifblocks)
            {
                yield return elseifblock;
            }
            if (Elseblock != null)
                yield return Elseblock;
        }
    }
}

