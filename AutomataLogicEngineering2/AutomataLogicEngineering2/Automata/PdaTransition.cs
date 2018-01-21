namespace AutomataLogicEngineering2.Automata
{
    public class PdaTransition : Transition
    {
        public char PopStack { get; }

        public char PutStack { get; }

        public PdaTransition(
            char transitionChar, State transitionFrom, State transitionTo, char popStack, char putOnStack)
            : base(transitionChar, transitionFrom, transitionTo)
        {
            this.PopStack = popStack;
            this.PutStack = putOnStack;
        }

        public override string GetTextForGraphLabel() => $"{this.TransitionChar} [{this.PopStack}/{this.PutStack}]";
    }
}
