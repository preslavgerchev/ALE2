namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;

    public class Alphabet
    {
        private const char Epsilon = 'ε';

        public IReadOnlyList<char> AlphabetChars { get; }

        public Alphabet(IReadOnlyList<char> alphabetChars)
        {
            this.AlphabetChars = alphabetChars;
        }

        public bool Contains(char alphabetChar)
        {
            return AlphabetChars.Concat(new List<char>() {Epsilon}).Any(x => x == alphabetChar);
        }
    }
}
