namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using Automata;

    public static class FiniteAutomataParser
    {
        public static FiniteAutomata CreateAutomata(List<string> lines)
        {
            var cleanLines = lines
                .Where(x => !string.IsNullOrWhiteSpace(x)).Select(l => l.Replace(" ", string.Empty))
                .ToList();
            var comment = new CommentParser().Parse(lines);
            var alphabet = new AlphabetParser().Parse(cleanLines);
            var states = new StatesParser().Parse(cleanLines);
            var stack = new StackAlphabetParser().Parse(cleanLines);
            var transitions = new TransitionsParser(states, alphabet, stack != null).Parse(cleanLines);
            var testWords = new TestWordsParser().Parse(cleanLines);
            var shouldBeFinite = new IsFiniteParser().Parse(cleanLines);
            var shouldbeDfa = new IsDfaParser().Parse(cleanLines);
            foreach (var transition in transitions)
            {
                var stateFrom = states.SingleOrDefault(x => x.StateName == transition.TransitionFrom.StateName);
                stateFrom.Transitions.Add(transition);
            }

            // Having a stack means its a PDA.
            var automata = stack != null
                ? new PdaAutomata(comment, alphabet, states, testWords, shouldbeDfa, shouldBeFinite, stack)
                : new FiniteAutomata(comment, alphabet, states, testWords, shouldbeDfa, shouldBeFinite);
            automata.AcceptWords();
            return automata;
        }
    }
}
