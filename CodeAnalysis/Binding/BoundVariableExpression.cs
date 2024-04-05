using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        public BoundVariableExpression(VariableSymbole name)
        {

            Variable = name;
        }
        public override Type Type => Variable.Type;
        public override BoundNodeKind Kind => BoundNodeKind.NameExpression;

        public VariableSymbole Variable { get; }
    }
}