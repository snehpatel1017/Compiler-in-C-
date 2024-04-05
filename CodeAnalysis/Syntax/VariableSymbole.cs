namespace mc.CodeAnalysis.Syntax
{
    public sealed class VariableSymbole
    {
        internal VariableSymbole(string name, Type type, object? value, bool isReadOnly = false)
        {
            Name = name;
            Type = type;
            Value = value;
            IsReadOnly = isReadOnly;
        }
        public string Name { get; set; }
        public Type Type { get; set; }
        public object? Value { get; set; }
        public bool IsReadOnly { get; }
    }
}