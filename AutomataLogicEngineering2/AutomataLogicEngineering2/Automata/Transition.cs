namespace AutomataLogicEngineering2.Automata
{
    public class Transition
    {
        // TODO export epsilon to static variable?
        public bool IsEpsilon => this.TransitionChar == 'ε';

        public char TransitionChar { get; }

        public State TransitionTo { get; }

        public Transition(char transitionChar, State transitionTo)
        {
            this.TransitionChar = transitionChar == '_' ? 'ε' : transitionChar;
            this.TransitionTo = transitionTo;
        }
    }
}
