using System.Linq.Expressions;
using mc.CodeAnalysis.Syntax;

namespace mc.CodeAnalysis.Binding
{

    internal sealed class Binder
    {
        private DiagnosticBag _diagnostics = new DiagnosticBag();
        public DiagnosticBag Diagnostics => _diagnostics;
        public Binder()
        {

        }
        public BoundStatement BindStatement(StatementSyntax syntax, BoundScope scope)
        {

            switch (syntax.Kind)
            {
                case SyntaxKind.BlockStatement:
                    return BindBlockStatement((BlockStatement)syntax, scope);
                case SyntaxKind.ExpressionStatement:
                    return new BoundExpressionStatement(BindExpression(((ExpressionStatement)syntax)._expression, scope));
                case SyntaxKind.VariableDeclarationStatement:
                    return BindVariableDeclaration((VariableDeclaration)syntax, scope);
                case SyntaxKind.IfStatement:
                    return BindIfConditionStatement((IfStatementSyntax)syntax, scope);
                case SyntaxKind.WhileStatement:
                    return BindWhileStatement((WhileStatementSyntax)syntax, scope);
                default:
                    throw new Exception($"Unexpected syntax {syntax.Kind}");

            }
        }

        private BoundStatement BindWhileStatement(WhileStatementSyntax syntax, BoundScope scope)
        {
            var condition = BindExpression(syntax.Conditionstatement, scope, typeof(bool));
            var thenstatement = BindStatement(syntax.Thenstatement, scope);
            return new BoundWhileStatement(condition, thenstatement);
        }

        private BoundIfBlock BindIfConditionStatement(IfStatementSyntax syntax, BoundScope scope)
        {
            var condition = BindExpression(syntax.Conditionstatement, scope, typeof(bool));
            var thenstatement = BindStatement(syntax.Thenstatement, scope);
            List<BoundElifBlock> elifbounds = new List<BoundElifBlock>();
            foreach (var elifstatement in syntax.Elseifblocks)
            {
                var cd = BindExpression(elifstatement.Conditionstatement, scope, typeof(bool));
                var ts = BindStatement(elifstatement.Thenstatement, scope);
                elifbounds.Add(new BoundElifBlock(cd, ts));
            }
            BoundElseBlock? elsebound = null;
            if (syntax.Elseblock != null)
            {
                var ts = BindStatement(syntax.Elseblock.Thenstatement, scope);
                elsebound = new BoundElseBlock(ts);
            }
            return new BoundIfBlock(condition, thenstatement, elifbounds, elsebound);
        }

        private BoundExpression BindExpression(ExpressionSyntax conditionstatement, BoundScope scope, Type targettype)
        {
            var result = BindExpression(conditionstatement, scope);
            if (result.Type != targettype)
            {
                _diagnostics.ReportCannotConvert(conditionstatement.Span, result.Type, targettype);
            }
            return result;
        }

        public BoundVariableDeclaration BindVariableDeclaration(VariableDeclaration syntax, BoundScope scope)
        {
            var initializer = BindExpression(syntax.Initializer, scope);
            var variable = scope.IsExist(syntax.Identifier.Text);
            var isReadOnly = syntax.Keyword.Kind == SyntaxKind.ConstKeyword;
            if (variable == null)
            {
                variable = new VariableSymbole(syntax.Identifier.Text, initializer.Type, null, isReadOnly);
                scope.dictionary.Add(variable);

            }
            else
            {
                _diagnostics.ReportRedeclarationError(syntax.EqualToken.Span, variable, initializer.Type);

            }
            return new BoundVariableDeclaration(variable, initializer);
        }
        public BoundBlockStatement BindBlockStatement(BlockStatement syntax, BoundScope scope)
        {
            List<BoundStatement> statements = new List<BoundStatement>();
            BoundScope newscope = new BoundScope(scope);
            foreach (var statement in syntax._statements)
            {
                var stm = BindStatement(statement, newscope);
                statements.Add(stm);
            }
            return new BoundBlockStatement(statements, newscope);
        }
        public BoundExpression BindExpression(ExpressionSyntax syntax, BoundScope scope)
        {

            switch (syntax.Kind)
            {
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax, scope);
                case SyntaxKind.UniaryExpression:
                    return BindUnaryExpression((UniaryExpressionSyntax)syntax, scope);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax, scope);
                case SyntaxKind.ParanthesizedExpression:
                    return BindParanthesizedExpression((ParanthesizedExpressionSyntax)syntax, scope);
                case SyntaxKind.NameExpression:
                    return BindNameExpression((NameExpressionSyntax)syntax, scope);
                case SyntaxKind.AssignmentExpression:
                    return BindAssignmentExpression((AssignmentExpressionSyntax)syntax, scope);
                default:
                    throw new Exception($"Unexpected syntax {syntax.Kind}");
            }
        }
        private BoundExpression BindAssignmentExpression(AssignmentExpressionSyntax syntax, BoundScope scope)
        {
            var expression = BindExpression(syntax.Expression, scope);
            var name = syntax.IdentifierToken.Text;
            var variable = scope.Lookup(name);
            if (variable == null)
            {
                _diagnostics.ReportUndeclareVariableError(syntax.IdentifierToken.Span, syntax.IdentifierToken.Text);

            }
            else
            {

                if (variable.Type != expression.Type)
                {
                    _diagnostics.ReportTypemissMatch(syntax.IdentifierToken.Span, variable, expression.Type);
                }
                else if (variable.IsReadOnly)
                {
                    _diagnostics.ReportReadOnlyVariableError(syntax.IdentifierToken.Span, variable.Name);
                }

            }
            return new BoundAssignmentExpression(new VariableSymbole(name, expression.Type, null), expression);
        }
        private BoundExpression BindNameExpression(NameExpressionSyntax syntax, BoundScope scope)
        {

            var name = syntax.IdentifierToken.Text;
            var variable = scope.Lookup(name);
            if (variable == null)
            {
                _diagnostics.ReportUndefinedName(syntax.IdentifierToken.Span, name);
                return new BoundLiteralExpression(0);
            }

            return new BoundVariableExpression(variable);
        }
        private BoundExpression BindParanthesizedExpression(ParanthesizedExpressionSyntax syntax, BoundScope scope)
        {
            return BindExpression(syntax.Expression, scope);
        }
        private BoundLiteralExpression BindLiteralExpression(LiteralExpressionSyntax syntax, BoundScope scope)
        {
            var value = syntax.Value;
            if (value == null) value = 0;
            return new BoundLiteralExpression(syntax.Value);
        }
        private BoundExpression BindUnaryExpression(UniaryExpressionSyntax syntax, BoundScope scope)
        {
            var operand = BindExpression(syntax.Expression, scope);
            var unaryOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, operand.Type);
            if (unaryOperator == null)
            {

                _diagnostics.ReportUnexprectedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, operand.Type);
                return operand;
            }
            return new BoundUnaryExpression(unaryOperator, operand);
        }
        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax, BoundScope scope)
        {
            var left = BindExpression(syntax.Left, scope);
            var right = BindExpression(syntax.Right, scope);
            var binaryOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, left.Type, right.Type);
            if (binaryOperator == null)
            {
                _diagnostics.ReportUnexprectedBinaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, left.Type, right.Type);
                return left;
            }
            return new BoundBinaryExpression(left, binaryOperator, right);
        }
    }


}