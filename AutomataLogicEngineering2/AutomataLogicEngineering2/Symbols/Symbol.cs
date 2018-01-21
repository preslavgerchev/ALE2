namespace AutomataLogicEngineering2.Symbols
{
    /// <summary>
    /// A class that represents a single symbol on a node.
    /// </summary>
    public abstract class Symbol
    {
        /// <summary>
        /// Gets the char symbol.
        /// </summary>
        public char CharSymbol { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="charSymbol">The char symbol.</param>
        protected Symbol(char charSymbol)
        {
            this.CharSymbol = charSymbol;
        }

        /// <summary>
        /// Returns a textual representation of the symbol.
        /// </summary>
        /// <returns>The char symbol.</returns>
        public override string ToString() => this.CharSymbol.ToString();
    }
}
