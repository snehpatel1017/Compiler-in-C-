namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundElifBlock : BoundStatement
    {

        public BoundElifBlock(BoundExpression condition, BoundStatement thenstatement)
        {
            Condition = condition;
            Thenstatement = thenstatement;
        }

        public BoundExpression Condition { get; }
        public BoundStatement Thenstatement { get; }

        public override BoundNodeKind Kind => BoundNodeKind.ElifblockStatement;
    }
}