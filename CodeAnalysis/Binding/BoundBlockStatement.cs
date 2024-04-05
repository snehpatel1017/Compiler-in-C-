namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundBlockStatement : BoundStatement
    {
        public BoundBlockStatement(List<BoundStatement> statements, BoundScope scope)
        {
            _statements = statements;
            Scope = scope;
        }

        public List<BoundStatement> _statements { get; }
        public BoundScope Scope { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
    }
}