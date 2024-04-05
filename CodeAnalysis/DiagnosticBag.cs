using System;
using System.Collections;
using System.Collections.Generic;
using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();
        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(DiagnosticBag diagnostic)
        {
            _diagnostics.AddRange(diagnostic._diagnostics);
        }
        private void Report(TextSpan spane, string message)
        {
            var diagnostic = new Diagnostic(spane, message);
            _diagnostics.Add(diagnostic);
        }



        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}";
            Report(span, message);
        }
        public void ReportBadCharacter(int pos, char c)
        {
            var message = $"ERROR : bad character  input : {c}";
            Report(new TextSpan(pos, 1), message);
        }

        public void ReportUnexprectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            var message = $"ERROR : Unexprected Token of kind <{actualKind}> , Exprected : <{expectedKind}>";
            Report(span, message);
        }
        public void ReportUnexprectedUnaryOperator(TextSpan span, string operatorText, Type type)
        {
            var message = $"Unary operator {operatorText} is not defined for type {type}";
            Report(span, message);
        }

        public void ReportUnexprectedBinaryOperator(TextSpan span, string operatorText, Type left, Type right)
        {
            var message = $"Binary operator {operatorText} is not defined for type {left} and {right}";
            Report(span, message);
        }

        public void ReportUndefinedName(TextSpan span, string name)
        {
            var message = $"Undefined Variable {name}";
            Report(span, message);
        }

        public void ReportTypemissMatch(TextSpan span, VariableSymbole vs, Type expressiontype)
        {
            var message = $"cannot convert this {expressiontype} to {vs.Type} ";
            Report(span, message);
        }

        internal void ReportRedeclarationError(TextSpan span, VariableSymbole vs, Type expressiontype)
        {
            var message = $"there is already variable declared with '{vs.Name}' name and cannot convert this {expressiontype} to {vs.Type} ";
            Report(span, message);
        }

        internal void ReportUndeclareVariableError(TextSpan span, string name)
        {
            var message = $"there is no such variable declared with name {name}";
            Report(span, message);
        }

        internal void ReportReadOnlyVariableError(TextSpan span, string name)
        {
            var message = $"The Variable {name} is Read only Variable";
            Report(span, message);
        }

        internal void ReportMissingIfStatement(TextSpan span, string text)
        {
            var message = $"{text} is used without previous If statement";
            Report(span, message);
        }

        internal void ReportCannotConvert(TextSpan span, Type type, Type targettype)
        {
            var message = $"Cannot convert from {type}  to {targettype}";
            Report(span, message);
        }
    }
}
