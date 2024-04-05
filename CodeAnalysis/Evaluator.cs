using mc.CodeAnalysis.Binding;
using mc.CodeAnalysis.Syntax;
using System.Collections.Generic;


namespace mc.CodeAnalysis
{
    internal sealed class Evaluator
    {
        private readonly BoundStatement _root;
        private object _result;


        public Evaluator(BoundStatement root)
        {
            this._root = root;

        }
        public object Evaluate(BoundScope globalscope)
        {
            return EvaluateStatement(_root, globalscope);
        }
        private object EvaluateStatement(BoundStatement root, BoundScope scope)
        {
            if (root is BoundBlockStatement a)
            {
                foreach (var statement in a._statements)
                {
                    _result = EvaluateStatement(statement, a.Scope);
                }
            }
            else if (root is BoundVariableDeclaration b)
            {
                _result = EvaluateExpression(b.Initializer, scope);
                b.Variable.Value = _result;
                scope.dictionary.Add(b.Variable);
            }
            else if (root is BoundIfBlock c)
            {
                var condition = (bool)EvaluateExpression(c.Condition, scope);
                if (condition)
                {
                    _result = EvaluateStatement(c.Thenstatement, scope);
                }
                else
                {
                    var changed = false;
                    foreach (BoundElifBlock elb in c.Elifbounds)
                    {
                        var cd = (bool)EvaluateExpression(elb.Condition, scope);
                        if (cd)
                        {
                            _result = EvaluateStatement(elb.Thenstatement, scope);
                            changed = true;
                            break;
                        }
                    }
                    if (!changed && c.Elsebound != null)
                    {
                        _result = EvaluateStatement(c.Elsebound.Thenstatement, scope);
                    }

                }
            }
            else if (root is BoundWhileStatement d)
            {
                while ((bool)EvaluateExpression(d.Condition, scope))
                {
                    _result = EvaluateStatement(d.Thenstatement, scope);
                }
            }
            else if (root is BoundExpressionStatement rt)
            {
                _result = EvaluateExpression(rt._expression, scope);
            }
            return _result;
        }
        private object EvaluateExpression(BoundExpression root, BoundScope scope)
        {
            if (root is BoundVariableExpression n)
            {
                // var variable = _variables.Keys.FirstOrDefault(v => v.Name == n.Variable.Name);
                var variable = scope.Lookup(n.Variable.Name);
                if (variable == null)
                {
                    throw new Exception("there is no such variable " + n.Variable.Name);

                }
                else
                {
                    if (variable.Type != n.Type) throw new Exception("there is previous variable with same name but different data type");
                    return variable.Value;
                }
            }
            if (root is BoundAssignmentExpression e)
            {
                var value = EvaluateExpression(e.Expression, scope);
                // var variable = _variables.Keys.FirstOrDefault(v => v.Name == e.Variable.Name);
                var variable = scope.Lookup(e.Variable.Name);
                if (variable == null) variable = e.Variable;
                variable.Type = value.GetType();
                variable.Value = value;
                return value;
            }

            if (root is BoundLiteralExpression a)
                return a.Value;

            if (root is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand, scope);
                switch (u.Op.Kind)
                {
                    case BoundUnaryOperatorKind.Negation: return -(int)operand;
                    case BoundUnaryOperatorKind.Identity: return (int)operand;
                    case BoundUnaryOperatorKind.LogicalNot: return !(bool)operand;
                    case BoundUnaryOperatorKind.BitWiseNor: return ~(int)operand;
                    default:
                        throw new Exception($"Unexprected uniary operator to evaluate {u.Op.Kind}!!");
                }


            }

            if (root is BoundBinaryExpression b)
            {
                var left = EvaluateExpression(b.Left, scope);
                var right = EvaluateExpression(b.Right, scope);
                switch (b.Op.Kind)
                {
                    case BoundBinaryOperatorKind.Addition: return (int)left + (int)right;
                    case BoundBinaryOperatorKind.Multiplication: return (int)left * (int)right;
                    case BoundBinaryOperatorKind.Substraction: return (int)left - (int)right;
                    case BoundBinaryOperatorKind.Divsion: return (int)left / (int)right;
                    case BoundBinaryOperatorKind.LogicalAnd: return (bool)left && (bool)right;
                    case BoundBinaryOperatorKind.LogicalOr: return (bool)left || (bool)right;
                    case BoundBinaryOperatorKind.Equals: return Equals(left, right);
                    case BoundBinaryOperatorKind.NotEquals: return !Equals(left, right);
                    case BoundBinaryOperatorKind.Less: return (int)left < (int)right;
                    case BoundBinaryOperatorKind.LessOrEqualsTo: return (int)left <= (int)right;
                    case BoundBinaryOperatorKind.GreaterOrEqualsTo: return (int)left >= (int)right;
                    case BoundBinaryOperatorKind.Greater: return (int)left > (int)right;
                    case BoundBinaryOperatorKind.BitWiseAnd: return (int)left & (int)right;
                    case BoundBinaryOperatorKind.BitWiseOr: return (int)left | (int)right;
                    case BoundBinaryOperatorKind.BitWiseXor: return (int)left ^ (int)right;

                    default:
                        throw new Exception($"Unexprected binary operator to evaluate {b.Op.Kind}!!");
                }

            }


            throw new Exception($"Unexpected node {root.Kind}");
        }
    }
}