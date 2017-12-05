namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Automata;

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
            var states = new StatesParser().Parse(lines);
            var transitions = new TransitionsParser(states, alphabet).Parse(lines);
            foreach (var transition in transitions)
            {
                var stateFrom = states.SingleOrDefault(x => x.StateName == transition.TransitionFrom.StateName);
                stateFrom.Transitions.Add(transition);
            }

            return new FiniteAutomata(comment, alphabet, states);
        }
    }
}
