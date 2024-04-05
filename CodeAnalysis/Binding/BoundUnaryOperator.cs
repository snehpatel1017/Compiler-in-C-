using mc.CodeAnalysis.Syntax;

namespace mc.CodeAnalysis.Binding
{
    internal class BoundUnaryOperator
    {
        public BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type type) : this(syntaxKind, kind, type, type)
        {

        }
        public BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type opType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OpType = opType;
            ResultType = resultType;
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OpType { get; }
        public Type ResultType { get; }

        private static BoundUnaryOperator[] _operators = {
            new BoundUnaryOperator(SyntaxKind.BangToken,BoundUnaryOperatorKind.LogicalNot , typeof(bool)),
            new BoundUnaryOperator(SyntaxKind.PlusToken,BoundUnaryOperatorKind.Identity , typeof(int)),
            new BoundUnaryOperator(SyntaxKind.MinusToken,BoundUnaryOperatorKind.Negation , typeof(int)),
            new BoundUnaryOperator(SyntaxKind.TildeToken,BoundUnaryOperatorKind.BitWiseNor , typeof(int)),

        };

        public static BoundUnaryOperator Bind(SyntaxKind kind, Type opType)
        {
            foreach (var op in _operators)
            {
                if (op.SyntaxKind == kind && op.OpType == opType) return op;
            }
            return null;
        }


    }
}