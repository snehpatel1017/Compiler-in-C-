using mc.CodeAnalysis;
using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis.Syntax
{



    public sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }

        public override TextSpan Span => new TextSpan(Left.Span.Start, Right.Span.Start + Right.Span.Length - Left.Span.Start - 1);

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }

}