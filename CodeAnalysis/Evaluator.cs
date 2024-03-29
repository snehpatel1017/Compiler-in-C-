namespace Minsk.CodeAnalysis
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            this._root = root;
        }
        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }
        private int EvaluateExpression(ExpressionSyntax root)
        {
            if (root is LiteralExpressionSyntax n)
                return (int)n.LiteralToken.Value;

            if (root is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;

                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");

            }

            if (root is ParanthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }
            throw new Exception($"Unexpected node {root.Kind}");
        }
    }
}