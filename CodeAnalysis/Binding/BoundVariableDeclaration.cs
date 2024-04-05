using mc.CodeAnalysis.Syntax;

namespace mc.CodeAnalysis.Binding
{
    internal sealed class BoundVariableDeclaration : BoundStatement
    {
        public override BoundNodeKind Kind => BoundNodeKind.VariableDeclarationStatement;

        public VariableSymbole Variable { get; }
        public BoundExpression Initializer { get; }

        public BoundVariableDeclaration(VariableSymbole variable, BoundExpression initializer)
        {
            Variable = variable;
            Initializer = initializer;
        }
    }
}