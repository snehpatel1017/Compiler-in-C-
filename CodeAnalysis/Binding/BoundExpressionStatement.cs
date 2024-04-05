namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundExpressionStatement : BoundStatement
    {
        public BoundExpressionStatement(BoundExpression expression)
        {
            _expression = expression;
        }
        public BoundExpression _expression { get; }
        public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
    }
}