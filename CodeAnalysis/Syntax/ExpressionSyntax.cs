namespace mc.CodeAnalysis.Syntax
{
    public abstract class ExpressionSyntax : SyntaxNode
    {

        public abstract TextSpan Span { get; }
    }
}