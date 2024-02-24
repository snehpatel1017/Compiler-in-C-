using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using Minsk.CodeAnalysis;

namespace Minsk
{
    internal sealed class Program
    {
        static void Main()
        {
            bool showtree = false;
            while (true)
            {
                Console.Write(">:");
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    return;
                if (line == "toggleshow")
                {
                    showtree = !showtree;
                    continue;
                }
                else if (line == "cls")
                {
                    Console.Clear();
                    continue;
                }
                var syntaxtree = SyntaxTree.Parse(line);
                if (showtree)
                    PreetyPrint(syntaxtree.Root);

                if (syntaxtree.Diagnostics.Any())
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostics in syntaxtree.Diagnostics)
                    {
                        Console.WriteLine(diagnostics);
                    }

                    Console.ForegroundColor = color;
                }
                else
                {
                    var e = new Evaluator(syntaxtree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);

                }
            }
            static void PreetyPrint(SyntaxNode node, string indent = "")
            {
                Console.Write(indent);
                Console.Write(node.Kind);

                if (node is SyntaxToken t && t.Value != null)
                {
                    Console.Write(" ");
                    Console.Write(t.Value);
                }
                Console.WriteLine();
                indent += "|____";
                foreach (var child in node.GetChildren())
                {
                    PreetyPrint(child, indent);
                }
            }
        }
    }

}
