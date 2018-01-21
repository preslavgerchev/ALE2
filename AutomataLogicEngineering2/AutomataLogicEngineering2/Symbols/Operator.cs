namespace AutomataLogicEngineering2.Symbols
{
    /// <summary>
    /// A class, that represents a single connective.
    /// </summary>
    public class Operator : Symbol
    {
        /// <summary>
        /// Gets the connective type.
        /// </summary>
        public OperatorType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="charSymbol">The char symbol.</param>
        /// <param name="type">The connective type.</param>
        public Operator(char charSymbol, OperatorType type)
            : base(charSymbol)
        {
            this.Type = type;
        }
    }
}