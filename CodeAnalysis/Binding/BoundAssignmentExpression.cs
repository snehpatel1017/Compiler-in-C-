using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundAssignmentExpression : BoundExpression
    {
        public BoundAssignmentExpression(VariableSymbole name, BoundExpression expression)
        {
            Expression = expression;
            Variable = name;
        }
        public override Type Type => Expression.Type;
        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
        public BoundExpression Expression { get; }
        public VariableSymbole Variable { get; }

    }
}