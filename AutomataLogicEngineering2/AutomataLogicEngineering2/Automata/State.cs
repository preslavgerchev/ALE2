namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;

    public class State
    {
        public string StateName { get; }

        public IList<Transition> Transitions { get; }

        public bool IsFinal { get; set; }

        public State(string stateName)
        {
            this.StateName = stateName;
            this.Transitions = new List<Transition>();
            this.IsFinal = false;
        }
    }
}
