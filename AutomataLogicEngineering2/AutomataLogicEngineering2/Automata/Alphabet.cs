namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public class Alphabet
    {
        public IReadOnlyList<char> AlphabetChars { get; }

        public Alphabet(IReadOnlyList<char> alphabetChars)
        {
            this.AlphabetChars = alphabetChars;
        }

        public bool Contains(char alphabetChar)
        {
            return AlphabetChars.Concat(new List<char>() { Epsilon.Letter }).Any(x => x == alphabetChar);
        }
    }
}
