using System;
using System.Collections;
using System.Collections.Generic;
using mc.CodeAnalysis.Syntax;
using mc.CodeAnalysis.Binding;
namespace mc.CodeAnalysis
{
    public sealed class Compilation
    {
        public Compilation(SyntaxTree syntaxTree)
        {
            STree = syntaxTree;
        }
        public SyntaxTree STree { get; }
        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var GlobalScope = new BoundScope(null);
            var boundExpressoin = binder.BindStatement(STree.Root, GlobalScope);
            var diagnostics = STree.Diagnostics;
            diagnostics = diagnostics.Concat(binder.Diagnostics).ToArray();

            if (diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }
            else
            {
                var e = new Evaluator(boundExpressoin);
                var result = e.Evaluate(GlobalScope);
                return new EvaluationResult(Array.Empty<Diagnostic>(), result);
            }

        }
    }
}