using System;
using System.Data;
using mc.CodeAnalysis.Syntax;
using mc.CodeAnalysis;
using mc.CodeAnalysis.Text;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;


namespace mc
{
    internal sealed class Program
    {
        private static ObservableCollection<string> document = new ObservableCollection<string>();

        // static void Main()
        // {
        //     bool showtree = false;
        //     bool showsymbols = false;
        //     var variable = new Dictionary<VariableSymbole, object>();
        //     StringBuilder inputtext = new StringBuilder();
        //     while (true)
        //     {
        //         Console.ForegroundColor = ConsoleColor.Green;
        //         if (inputtext.Length == 0)
        //             Console.Write("» ");
        //         else
        //             Console.Write("· ");
        //         Console.ResetColor();
        //         var line = Console.ReadLine();
        //         var isBlank = string.IsNullOrWhiteSpace(line);
        //         if (inputtext.Length == 0)
        //         {
        //             if (isBlank) break;
        //             else if (line == "toggleshow")
        //             {
        //                 showtree = !showtree;
        //                 continue;
        //             }
        //             else if (line == "cls")
        //             {
        //                 Console.Clear();
        //                 continue;
        //             }


        //         }


        //         inputtext.AppendLine(line);
        //         string txt = inputtext.ToString();
        //         var syntaxtree = SyntaxTree.Parse(txt);
        //         if (!isBlank && syntaxtree.Diagnostics.Any()) continue;
        //         var compilation = new Compilation(syntaxtree);
        //         var result = compilation.Evaluate();
        //         IReadOnlyList<Diagnostic> diagnostics = (IReadOnlyList<Diagnostic>)result.Diagnostics;
        //         if (showtree)
        //             PreetyPrint(syntaxtree.Root);



        //         if (diagnostics.Any())
        //         {
        //             List<TextLine> stx = syntaxtree.stx._text;

        //             var original = Console.ForegroundColor;
        //             foreach (var diagnostic in diagnostics)
        //             {

        //                 int linenumber = SourceText.GetLineNumber(diagnostic.Span.Start, stx);
        //                 int index = diagnostic.Span.Start - stx[linenumber - 1].Start + stx[linenumber - 1].LastlineBreakWidth;
        //                 Console.ForegroundColor = ConsoleColor.DarkRed;
        //                 Console.Write("(");
        //                 Console.Write(linenumber + " , " + index);
        //                 Console.Write(") ");
        //                 Console.WriteLine(diagnostic);
        //                 Console.WriteLine(diagnostic.Span.Length);
        //                 Console.ForegroundColor = original;
        //                 int errorlimit = Math.Min(diagnostic.Span.Length, stx[linenumber - 1].Length);
        //                 var prefix = stx[linenumber - 1].Text.Substring(0, index);
        //                 var error = stx[linenumber - 1].Text.Substring(index, errorlimit);
        //                 var suffix = stx[linenumber - 1].Text.Substring(index + errorlimit);
        //                 Console.Write("     ");
        //                 Console.Write(prefix);
        //                 Console.ForegroundColor = ConsoleColor.DarkRed;
        //                 Console.Write(error);
        //                 Console.ForegroundColor = original;
        //                 Console.WriteLine(suffix);


        //             }
        //             Console.WriteLine();
        //         }
        //         else
        //         {
        //             Console.ForegroundColor = ConsoleColor.DarkMagenta;
        //             Console.WriteLine(result.Value);
        //             Console.ResetColor();
        //         }
        //         inputtext.Clear();


        //     }
        //     static void PreetyPrint(SyntaxNode node, string indent = "")
        //     {
        //         Console.Write(indent);
        //         Console.Write(node.Kind);

        //         if (node is SyntaxToken t && t.Value != null)
        //         {
        //             Console.Write(" ");
        //             Console.Write(t.Value);
        //         }
        //         Console.WriteLine();
        //         indent += "|____";
        //         foreach (var child in node.GetChildren())
        //         {
        //             PreetyPrint(child, indent);
        //         }
        //     }
        // }


        static void Main()
        {
            document.Add("");
            var view = new SubmissionView(document);
            while (true)
            {
                var Key = Console.ReadKey(true);
                HandleKey(Key, document, view);
            }
        }

        private static void Run(SubmissionView? view)
        {


            string Code = string.Join(Environment.NewLine, document);
            // document.Clear();
            var syntaxtree = SyntaxTree.Parse(Code);
            var compilation = new Compilation(syntaxtree);
            var result = compilation.Evaluate();
            IReadOnlyList<Diagnostic> diagnostics = (IReadOnlyList<Diagnostic>)result.Diagnostics;
            if (diagnostics.Any())
            {
                List<TextLine> stx = syntaxtree.stx._text;

                var original = Console.ForegroundColor;
                foreach (var diagnostic in diagnostics)
                {

                    int linenumber = SourceText.GetLineNumber(diagnostic.Span.Start, stx);
                    int index = diagnostic.Span.Start - stx[linenumber - 1].Start + stx[linenumber - 1].LastlineBreakWidth;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("(");
                    Console.Write(linenumber + " , " + index);
                    Console.Write(") ");
                    Console.WriteLine(diagnostic);
                    Console.WriteLine(diagnostic.Span.Length);
                    Console.ForegroundColor = original;
                    int errorlimit = Math.Min(diagnostic.Span.Length, stx[linenumber - 1].Length);
                    var prefix = stx[linenumber - 1].Text.Substring(0, index);
                    var error = stx[linenumber - 1].Text.Substring(index, errorlimit);
                    var suffix = stx[linenumber - 1].Text.Substring(index + errorlimit);
                    Console.Write("     ");
                    Console.Write(prefix);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(error);
                    Console.ForegroundColor = original;
                    Console.WriteLine(suffix);


                }
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(result.Value);
                Console.ResetColor();
            }
        }

        private static void HandleKey(ConsoleKeyInfo key, ObservableCollection<string> document, SubmissionView view)
        {
            if (key.Modifiers == default(ConsoleModifiers))
            {
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        HandleEnter(document, view);
                        break;
                    case ConsoleKey.UpArrow:
                        HandleUpArrow(document, view);
                        break;
                    case ConsoleKey.DownArrow:
                        HandleDownArrow(document, view);
                        break;
                    case ConsoleKey.LeftArrow:
                        HandleLeftArrow(document, view);
                        break;
                    case ConsoleKey.RightArrow:
                        HandleRightArrow(document, view);
                        break;
                    case ConsoleKey.Backspace:
                        HandleBackspace(document, view);
                        break;
                    case ConsoleKey.Delete:
                        HandleDelete(document, view);
                        break;

                }
            }
            else if (key.Modifiers == ConsoleModifiers.Control)
            {
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Run(view);
                        break;

                }
            }
            if (key.KeyChar >= ' ')
                HandleTyping(document, view, key.KeyChar.ToString());

        }

        private static void HandleDelete(ObservableCollection<string> document, SubmissionView view)
        {
            var index = view.CurrentCharacter;
            var lineindex = view.CurrentLineIndex;
            var line = document[lineindex];
            if (line.Length <= index) return;
            var before = line.Substring(0, index);
            var after = line.Substring(index + 1);
            document[lineindex] = before + after;

        }

        private static void HandleBackspace(ObservableCollection<string> document, SubmissionView view)
        {
            var index = view.CurrentCharacter;
            if (index == 0) return;
            var lineindex = view.CurrentLineIndex;
            var line = document[lineindex];
            var before = line.Substring(0, index - 1);
            var after = line.Substring(index);
            document[lineindex] = before + after;
            view.CurrentCharacter--;
        }

        private static void HandleRightArrow(ObservableCollection<string> document, SubmissionView view)
        {
            var line = document[view.CurrentLineIndex];
            if (view.CurrentCharacter < line.Length - 1)
                view.CurrentCharacter++;

        }

        private static void HandleLeftArrow(ObservableCollection<string> document, SubmissionView view)
        {
            if (view.CurrentCharacter > 0)
                view.CurrentCharacter--;
        }

        private static void HandleDownArrow(ObservableCollection<string> document, SubmissionView view)
        {

            if (view.CurrentLineIndex < document.Count - 1)
                view.CurrentLineIndex++;

        }

        private static void HandleUpArrow(ObservableCollection<string> document, SubmissionView view)
        {
            if (view.CurrentLineIndex > 0)
                view.CurrentLineIndex--;


        }

        private static void HandleEnter(ObservableCollection<string> document, SubmissionView view)
        {
            document.Add("");
            view.CurrentLineIndex = document.Count - 1;
            view.CurrentCharacter = 0;
        }

        private static void HandleTyping(ObservableCollection<string> document, SubmissionView view, string v)
        {
            var lineindex = view.CurrentLineIndex;
            var offset = view.CurrentCharacter;
            document[lineindex] = document[lineindex].Insert(offset, v);
            view.CurrentCharacter += v.Length;

        }
    }



}
