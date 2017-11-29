namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;

    public class State
    {
        public string StateName { get; }

        public List<Transition> Transitions { get; }

        public bool IsFinal { get; set; }

        public State(string stateName, List<Transition> transitions, bool isFinal)
        {
            this.StateName = stateName;
            this.Transitions = transitions;
            this.IsFinal = isFinal;
        }
    }
}
