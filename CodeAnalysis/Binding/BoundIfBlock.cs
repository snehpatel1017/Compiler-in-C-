namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundIfBlock : BoundStatement
    {
        public BoundIfBlock(BoundExpression condition, BoundStatement thenstatement, List<BoundElifBlock> elifbounds, BoundElseBlock? elsebound)
        {
            Condition = condition;
            Thenstatement = thenstatement;
            Elifbounds = elifbounds;
            Elsebound = elsebound;
        }

        public override BoundNodeKind Kind => BoundNodeKind.IfblockStatement;

        public BoundExpression Condition { get; }
        public BoundStatement Thenstatement { get; }
        public List<BoundElifBlock> Elifbounds { get; }
        public BoundElseBlock? Elsebound { get; }
    }
}