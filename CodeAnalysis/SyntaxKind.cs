namespace Minsk.CodeAnalysis
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

        //expression tokens
        BinaryExpression,
        LiteralExpression,
        ParanthesizedExpression
    }
}