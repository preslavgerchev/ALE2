namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Automata;
    using Exceptions;

    public static class FiniteAutomataParser
    {
        public static FiniteAutomata ParseAutomata(string textFilePath)
        {
            var lines = File.ReadLines(textFilePath);

            // TODO remove empty whitespaces everywhere
            return ValidateAutomata(lines.ToList());
        }

        // TODO improve code here
        private static FiniteAutomata ValidateAutomata(List<string> lines)
        {
            var comment = new CommentParser().Parse(lines);
            var alphabet = new AlphabetParser().Parse(lines);
            var states = new AllStatesParser().Parse(lines);
            var finalStates = new FinalStatesParser().Parse(lines);
            var transitions = new TransitionsParser(states).Parse(lines);
            foreach (var finalState in finalStates)
            {
                var state = states.SingleOrDefault(x => x.StateName == finalState.StateName);
                state.IsFinal = state != null
                    ? true
                    : throw new InvalidStateException($"State '{finalState.StateName}' does not exist.");
            }
            foreach (var transition in transitions)
            {
                var stateFrom = states.SingleOrDefault(x => x.StateName == transition.TransitionFrom.StateName);
                var stateTo = states.SingleOrDefault(x => x.StateName == transition.TransitionTo.StateName);
                if (stateTo == null)
                {
                    throw new InvalidStateException($"State '{transition.TransitionTo.StateName}' does not exist.");
                }
                if (stateFrom == null)
                {
                    throw new InvalidStateException($"State '{transition.TransitionFrom.StateName}' does not exist.");
                }
                if (!alphabet.Contains(transition.TransitionChar))
                {
                    throw new InvalidCharException(
                        $"Character '{transition.TransitionChar}' does not exist in the alphabet.");
                }
                stateFrom.Transitions.Add(transition);
            }

            return new FiniteAutomata(comment, alphabet, states);
        }
    }
}
