using System.Linq.Expressions;
using mc.CodeAnalysis;
using mc.CodeAnalysis.Text;

namespace mc.CodeAnalysis.Syntax
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        public SourceText stx { get; }
        private int _position;

        private DiagnosticBag _diagnostics = new DiagnosticBag();

        public DiagnosticBag Diagnostics => _diagnostics;

        public Parser(string text)
        {
            stx = new SourceText(text);

            var tokens = new List<SyntaxToken>();
            SyntaxToken token;
            var lexer = new Lexer(stx.Text);
            do
            {
                token = lexer.Lex();
                if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EndOfFileToken);
            _diagnostics.AddRange(lexer.Diagnostics);
            _tokens = tokens.ToArray();
        }

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];
            return _tokens[index];
        }
        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind) return NextToken();
            _diagnostics.ReportUnexprectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, Current.Position, null, null);
        }
        public SyntaxTree Parse()
        {
            var expression = ParseStatement();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expression, endOfFileToken, stx);
        }

        private StatementSyntax ParseStatement()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.OpenCurlyBrace:
                    return ParseBlockStatement();
                case SyntaxKind.IfKeyword:
                case SyntaxKind.ElifKeyword:
                case SyntaxKind.ElseKeyword:
                    return ParseConditionStatements();
                case SyntaxKind.WhileKeyword:
                    return ParseWhileStatement();
                case SyntaxKind.VarKeyword:
                case SyntaxKind.ConstKeyword:
                    return ParseVariableDeclaration();
                default:
                    var expression = ParseExpression();
                    return new ExpressionStatement(expression);

            }
        }

        private StatementSyntax ParseWhileStatement()
        {
            var keyword = MatchToken(SyntaxKind.WhileKeyword);
            var openbrace = MatchToken(SyntaxKind.OpenParanthesisToken);
            var condition = ParseExpression();
            var closebrace = MatchToken(SyntaxKind.CloseParanthesisToken);
            var thenstatement = ParseStatement();
            return new WhileStatementSyntax(keyword, openbrace, condition, closebrace, thenstatement);
        }

        private StatementSyntax ParseConditionStatements()
        {
            var isIf = Current.Kind == SyntaxKind.IfKeyword;
            if (!isIf)
            {
                _diagnostics.ReportMissingIfStatement(Current.Span, Current.Text);
                throw new Exception("Missing If statement !!");
            }

            var IfKeyword = MatchToken(SyntaxKind.IfKeyword);
            var openbrace = MatchToken(SyntaxKind.OpenParanthesisToken);
            var condition = ParseExpression();
            var closebrace = MatchToken(SyntaxKind.CloseParanthesisToken);
            var thenstatement = ParseStatement();
            List<ElifStatementSyntax> elifblocks = new List<ElifStatementSyntax>();
            while (Current.Kind == SyntaxKind.ElifKeyword)
            {
                var ElifKeyword = MatchToken(SyntaxKind.ElifKeyword);
                var ob = MatchToken(SyntaxKind.OpenParanthesisToken);
                var cnd = ParseExpression();
                var cb = MatchToken(SyntaxKind.CloseParanthesisToken);
                var ts = ParseStatement();
                elifblocks.Add(new ElifStatementSyntax(ElifKeyword, ob, cnd, cb, ts));
            }
            ElseStatementSyntax? elseblock = null;
            if (Current.Kind == SyntaxKind.ElseKeyword)
            {
                var elsekeyword = MatchToken(SyntaxKind.ElseKeyword);
                var ts = ParseStatement();
                elseblock = new ElseStatementSyntax(elsekeyword, ts);
            }
            return new IfStatementSyntax(IfKeyword, openbrace, condition, closebrace, thenstatement, elifblocks, elseblock);
        }


        private StatementSyntax ParseVariableDeclaration()
        {
            var expected = Current.Kind == SyntaxKind.VarKeyword ? SyntaxKind.VarKeyword : SyntaxKind.ConstKeyword;
            var keyword = MatchToken(expected);
            var identifier = MatchToken(SyntaxKind.IdentifierToken);
            var equalToken = MatchToken(SyntaxKind.EqualToken);
            var initializer = ParseExpression();
            return new VariableDeclaration(keyword, identifier, equalToken, initializer);
        }


        private BlockStatement ParseBlockStatement()
        {
            var openCurlyBrace = NextToken();
            List<StatementSyntax> statements = new List<StatementSyntax>();
            while (Current.Kind != SyntaxKind.EndOfFileToken && Current.Kind != SyntaxKind.CloseCurlyBrace)
            {
                var expressionStatement = ParseStatement();
                statements.Add(expressionStatement);

            }
            var closeCurlyBrace = MatchToken(SyntaxKind.CloseCurlyBrace);
            return new BlockStatement(openCurlyBrace, statements, closeCurlyBrace);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseAssignmentExpression();
        }

        private ExpressionSyntax ParseAssignmentExpression()
        {
            if (Peek(0).Kind == SyntaxKind.IdentifierToken && Peek(1).Kind == SyntaxKind.EqualToken)
            {
                var identiferToken = NextToken();
                var equalToken = NextToken();
                var right = ParseAssignmentExpression();
                return new AssignmentExpressionSyntax(identiferToken, equalToken, right);
            }
            return ParseBinaryExpression();

        }

        private ExpressionSyntax ParseBinaryExpression(int parentPrecedance = 0)
        {
            ExpressionSyntax left;
            var unary = Current.Kind.GetUnaryoperatorPrecedence();
            if (unary != 0 && unary >= parentPrecedance)
            {
                var operatorToken = NextToken();
                var operand = ParseBinaryExpression(unary);
                left = new UniaryExpressionSyntax(operatorToken, operand);
            }
            else
                left = ParsePrimaryExpression();
            while (true)
            {
                var precedence = Current.Kind.GetBinaryoperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedance)
                    break;

                var operatorToken = NextToken();
                var right = ParseBinaryExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);

            }
            return left;
        }




        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParanthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.CloseParanthesisToken);
                return new ParanthesizedExpressionSyntax(left, expression, right);
            }

            if (Current.Kind == SyntaxKind.FalseKeyword || Current.Kind == SyntaxKind.TrueKeyword)
            {
                var value = Current.Kind == SyntaxKind.TrueKeyword;
                var keyword = NextToken();
                return new LiteralExpressionSyntax(keyword, value);
            }

            if (Current.Kind == SyntaxKind.IdentifierToken)
            {
                var identifier = NextToken();
                return new NameExpressionSyntax(identifier);
            }

            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
}