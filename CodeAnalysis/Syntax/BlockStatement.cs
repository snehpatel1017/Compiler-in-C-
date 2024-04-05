namespace mc.CodeAnalysis.Syntax
{
    public sealed class BlockStatement : StatementSyntax
    {
        public BlockStatement(SyntaxToken openCurlyBrace, List<StatementSyntax> statements, SyntaxToken closeCurlyBrace)
        {
            _openCurlyBrace = openCurlyBrace;
            _statements = statements;
            _closeCurlyBrace = closeCurlyBrace;
        }
        public SyntaxToken _openCurlyBrace { get; }
        public List<StatementSyntax> _statements { get; }
        public SyntaxToken _closeCurlyBrace { get; }
        public override SyntaxKind Kind => SyntaxKind.BlockStatement;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return _openCurlyBrace;
            foreach (var statement in _statements)
            {
                yield return statement;
            }

            yield return _closeCurlyBrace;
        }
    }
}