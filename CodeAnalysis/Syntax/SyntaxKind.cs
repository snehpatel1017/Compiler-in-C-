namespace mc.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        BadToken,
        //special tokens
        EndOfFileToken,
        NumberToken,
        WhiteSpaceToken,

        //operator tokens
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParanthesisToken,
        CloseParanthesisToken,
        OpenCurlyBrace,
        CloseCurlyBrace,
        BangToken,
        EqualsEqualsToken,
        BangEqualsToken,
        EqualToken,
        LessToken,
        LessOrEqualsToken,
        GreaterToken,
        GreaterOrEqualsToken,
        IdentifierToken,


        //keywords
        TrueKeyword,
        FalseKeyword,
        VarKeyword,
        ConstKeyword,
        IfKeyword,
        ElifKeyword,
        ElseKeyword,
        WhileKeyword,


        //expression tokens
        BinaryExpression,
        LiteralExpression,
        ParanthesizedExpression,
        UniaryExpression,
        PipePipeToken,
        AmpersandAmpersandToken,

        NameExpression,
        AssignmentExpression,

        //statements
        BlockStatement,
        ExpressionStatement,
        VariableDeclarationStatement,
        IfStatement,
        ElifStatement,
        ElseStatement,
        WhileStatement,
        PipeToken,
        AmpersandToken,
        CapToken,
        TildeToken,
    }
}