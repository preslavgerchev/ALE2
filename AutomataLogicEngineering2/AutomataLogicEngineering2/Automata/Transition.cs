namespace AutomataLogicEngineering2.Automata
{
    using System;

    public class Transition : IEquatable<Transition>
    {
        private const char Epsilon = 'ε';

        public bool IsEpsilon => this.TransitionChar == Epsilon;

        public char TransitionChar { get; }

        public State TransitionFrom { get; }

        public State TransitionTo { get; }

        public Transition(char transitionChar, State transitionFrom, State transitionTo)
        {
            this.TransitionChar = transitionChar == '_' ? Epsilon : transitionChar;
            this.TransitionFrom = transitionFrom;
            this.TransitionTo = transitionTo;
        }

        public bool Equals(Transition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.TransitionChar == other.TransitionChar;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((Transition) obj);
        }

        public override int GetHashCode()
        {
            // TODO ?
            unchecked
            {
                return (TransitionChar.GetHashCode() * 397) ^ (TransitionTo != null ? TransitionTo.GetHashCode() : 0);
            }
        }
    }
}
