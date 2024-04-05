using mc.CodeAnalysis.Syntax;

namespace mc.CodeAnalysis.Binding
{
    internal class BoundBinaryOperator
    {
        public BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type) : this(syntaxKind, kind, type, type, type)
        {

        }
        public BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType, Type resultType) : this(syntaxKind, kind, operandType, operandType, resultType)
        {

        }
        public BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftTyepe, Type rightType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftTyepe = leftTyepe;
            RightType = rightType;
            ResultType = resultType;
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftTyepe { get; }
        public Type RightType { get; }
        public Type ResultType { get; }

        private static BoundBinaryOperator[] _operators = {
            new BoundBinaryOperator(SyntaxKind.AmpersandAmpersandToken,BoundBinaryOperatorKind.LogicalAnd , typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.PipePipeToken,BoundBinaryOperatorKind.LogicalOr , typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.PipePipeToken,BoundBinaryOperatorKind.Equals , typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.PipePipeToken,BoundBinaryOperatorKind.NotEquals , typeof(bool)),

            new BoundBinaryOperator(SyntaxKind.LessOrEqualsToken,BoundBinaryOperatorKind.LessOrEqualsTo , typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.LessToken,BoundBinaryOperatorKind.Less , typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.GreaterOrEqualsToken,BoundBinaryOperatorKind.GreaterOrEqualsTo , typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.GreaterToken,BoundBinaryOperatorKind.Greater , typeof(int),typeof(bool)),

            new BoundBinaryOperator(SyntaxKind.AmpersandToken,BoundBinaryOperatorKind.BitWiseAnd , typeof(int)),
            new BoundBinaryOperator(SyntaxKind.PipeToken,BoundBinaryOperatorKind.BitWiseOr , typeof(int)),
            new BoundBinaryOperator(SyntaxKind.CapToken,BoundBinaryOperatorKind.BitWiseXor , typeof(int)),

            new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken,BoundBinaryOperatorKind.Equals , typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.BangEqualsToken,BoundBinaryOperatorKind.NotEquals , typeof(int),typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.PlusToken,BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.MinusToken,BoundBinaryOperatorKind.Substraction, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.StarToken,BoundBinaryOperatorKind.Multiplication , typeof(int)),
            new BoundBinaryOperator(SyntaxKind.SlashToken,BoundBinaryOperatorKind.Divsion , typeof(int))

        };

        public static BoundBinaryOperator Bind(SyntaxKind kind, Type leftType, Type rightType)
        {
            foreach (var op in _operators)
            {
                if (op.SyntaxKind == kind && op.LeftTyepe == leftType && op.RightType == rightType) return op;
            }
            return null;
        }


    }
}