namespace mc.CodeAnalysis.Binding
{
    internal class BoundWhileStatement : BoundStatement
    {
        public BoundWhileStatement(BoundExpression condition, BoundStatement thenstatement)
        {
            Condition = condition;
            Thenstatement = thenstatement;
        }
        public override BoundNodeKind Kind => BoundNodeKind.WhileBlockStatement;

        public BoundExpression Condition { get; }
        public BoundStatement Thenstatement { get; }
    }
}