using mc.CodeAnalysis.Text;
using mc.CodeAnalysis;
namespace mc.CodeAnalysis.Syntax
{
    public sealed class SyntaxTree
    {
        public SourceText stx { get; }
        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, StatementSyntax root, SyntaxToken endOfFileToken, SourceText text)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
            stx = text;
        }

        public static SyntaxTree Parse(string line)
        {
            var p = new Parser(line);

            return p.Parse();
        }

        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public StatementSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
    }
}