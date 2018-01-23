namespace AutomataLogicEngineering2.Symbols
{
    public abstract class Symbol
    {
        public char CharSymbol { get; }

        protected Symbol(char charSymbol)
        {
            this.CharSymbol = charSymbol;
        }

        public override string ToString() => this.CharSymbol.ToString();
    }
}
