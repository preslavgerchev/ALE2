namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class State : IEquatable<State>
    {
        public string StateName { get; }

        public IList<Transition> Transitions { get; }

        public bool IsFinal { get; }

        public bool IsInitial { get; }

        public State(string stateName, bool isInitial = false, bool isFinal = false)
        {
            this.StateName = stateName;
            this.IsInitial = isInitial;
            this.Transitions = new List<Transition>();
            this.IsFinal = isFinal;
        }

        public bool Equals(State other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(StateName, other.StateName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((State) obj);
        }

        public override int GetHashCode()
        {
            return (StateName != null ? StateName.GetHashCode() : 0);
        }

        public List<State> PossibleEpsilonStates()
        {
            var epsilonTransitions = this.Transitions.Where(x => x.IsEpsilon).ToList();
            return epsilonTransitions.Select(x => x.TransitionTo).ToList();
        }

        public bool IsDfa(Alphabet alphabet)
        {
            if (this.Transitions.Count(x => !x.IsEpsilon) != alphabet.AlphabetChars.Count) return false;

            return alphabet.AlphabetChars.All(ch => this.Transitions.Any(tr => tr.TransitionChar == ch));
        }
    }
}
