namespace mc.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        LiteralExpression,
        BoundUnaryExpression,
        BoundBinaryExpression,
        NameExpression,
        AssignmentExpression,
        BlockStatement,
        ExpressionStatement,
        VariableDeclarationStatement,
        IfblockStatement,
        ElifblockStatement,
        ElseblockStatement,
        WhileBlockStatement,
    }
}