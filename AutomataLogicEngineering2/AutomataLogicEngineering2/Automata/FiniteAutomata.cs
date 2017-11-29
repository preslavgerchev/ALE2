namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;

    public class FiniteAutomata
    {
        public string Comment { set; get; }

        public List<char> Alphabet { set; get; }

        // TODO change this to State class.
        public List<State> States { get; set; }

        public bool IsDfa { get; set; }

        public bool IsNdfa => !this.IsDfa;

        // TODO add c-tor parameters.
        public FiniteAutomata()
        {

        }

        public void DetermineDfa()
        {
            foreach (var state in this.States)
            {
                if (this.Alphabet
                    .Any(ch => state.Transitions.Where(t => !t.IsEpsilon).Count(y => y.TransitionChar == ch) != 1))
                {
                    this.IsDfa = false;
                    return;
                }
            }
            this.IsDfa = true;
        }
    }
}
