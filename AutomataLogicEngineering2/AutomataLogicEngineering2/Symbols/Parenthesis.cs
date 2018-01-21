namespace AutomataLogicEngineering2.Symbols
{
    /// <summary>
    /// A class that represents a single opening or a closing parenthesis.
    /// </summary>
    public class Parenthesis : Symbol
    {
        public ParenthesisSide Side { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parenthesis"/> class.
        /// </summary>
        /// <param name="charSymbol">The char symbol.</param>
        /// <param name="side">The parenthesis side.</param>
        public Parenthesis(char charSymbol, ParenthesisSide side)
            : base(charSymbol)
        {
            this.Side = side;
        }
    }
}
