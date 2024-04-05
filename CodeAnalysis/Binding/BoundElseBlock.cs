namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundElseBlock : BoundStatement
    {
        public BoundElseBlock(BoundStatement thenstatement)
        {
            Thenstatement = thenstatement;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ElseblockStatement;

        public BoundStatement Thenstatement { get; }
    }
}