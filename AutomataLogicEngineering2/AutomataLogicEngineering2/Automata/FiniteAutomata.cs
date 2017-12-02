namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;

    public class FiniteAutomata
    {
        public string Comment { get; }

        public Alphabet Alphabet { get; }

        public IReadOnlyList<State> States { get; }

        public bool IsDfa { get; }

        public bool IsNdfa => !this.IsDfa;

        // TODO add c-tor parameters.
        public FiniteAutomata(string comment, Alphabet alphabet, IReadOnlyList<State> states)
        {
            this.Comment = comment;
            this.Alphabet = alphabet;
            this.States = states;
            this.IsDfa = this.DetermineDfa();
        }

        private bool DetermineDfa()
        {
            foreach (var state in this.States)
            {
                // If the amount of distinct transitions, excluding the epsilon one is different than the alphabet 
                // then we do not have  DFA.
                if (state.Transitions.Where(t => !t.IsEpsilon).Distinct().Count() != this.Alphabet.AlphabetChars.Count)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
