using mc.CodeAnalysis.Syntax;
namespace mc.CodeAnalysis.Binding
{
    public sealed class BoundScope
    {
        public List<VariableSymbole> dictionary = new List<VariableSymbole>();
        public BoundScope? _parent;

        public BoundScope(BoundScope? parent)
        {
            _parent = parent;
        }

        public VariableSymbole Lookup(string name)
        {
            var variable = dictionary.Find(v => v.Name == name);
            if (variable != null) return variable;
            if (_parent == null) return null;
            return _parent.Lookup(name);
        }

        public VariableSymbole IsExist(string name)
        {
            var variable = dictionary.Find(v => v.Name == name);
            return variable;
        }

    }
}