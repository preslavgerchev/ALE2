namespace AutomataLogicEngineering2.Symbols
{
    public class Operator : Symbol
    {
        public OperatorType Type { get; }

        public Operator(char charSymbol, OperatorType type)
            : base(charSymbol)
        {
            this.Type = type;
        }
    }
}