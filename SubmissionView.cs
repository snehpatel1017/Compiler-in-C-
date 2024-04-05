using System.Collections.ObjectModel;
using System.Collections.Specialized;

internal sealed class SubmissionView
{
    private readonly ObservableCollection<string> _document;
    private int _currentLineindex;
    private int _currentCharacter;
    public readonly int _cursourTop;

    public int _renderedLineCount;

    public SubmissionView(ObservableCollection<string> document)
    {
        _document = document;
        _document.CollectionChanged += SubmissionDocumentChnaged;
        _cursourTop = Console.CursorTop;

        Render();
    }

    private void SubmissionDocumentChnaged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Render();
    }

    private void Render()
    {

        Console.CursorVisible = false;
        var lineCount = 0;
        foreach (var line in _document)
        {
            Console.SetCursorPosition(0, _cursourTop + lineCount);
            Console.ForegroundColor = ConsoleColor.Green;

            if (lineCount == 0)
            {
                Console.Write("» ");
            }
            else
            {
                Console.Write("· ");
            }

            Console.ResetColor();
            Console.WriteLine(line + new string(' ', Console.WindowWidth - line.Length));
            lineCount++;
        }
        var blanklines = _renderedLineCount - lineCount;
        if (blanklines > 0)
        {
            var blank = new string(' ', Console.WindowWidth);
            while (blanklines > 0)
            {
                Console.WriteLine(blank);
            }
        }

        _renderedLineCount = lineCount;
        Console.CursorVisible = true;
        UpdateCursor();
    }

    public int CurrentLineIndex
    {
        get => _currentLineindex;
        set
        {
            if (_currentLineindex != value)
            {
                _currentLineindex = value;
                UpdateCursor();
            }
        }
    }


    public int CurrentCharacter
    {
        get => _currentCharacter;
        set
        {
            if (_currentCharacter != value)
            {
                _currentCharacter = value;
                UpdateCursor();
            }
        }
    }
    private void UpdateCursor()
    {
        Console.CursorTop = _cursourTop + _currentLineindex;
        Console.CursorLeft = 2 + _currentCharacter;
    }

}
