namespace AutomataLogicEngineering2.Symbols
{
    /// <summary>
    /// A class, that represents a predicate, usually represented by a single letter.
    /// </summary>
    public class Predicate : Symbol
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Predicate"/> class.
        /// </summary>
        /// <param name="charSymbol">The char symbol.</param>
        public Predicate(char charSymbol)
            : base(charSymbol)
        {
        }
    }
}
