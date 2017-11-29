namespace AutomataLogicEngineering2.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Automata;

    public static class FiniteAutomataParser
    {

        public static FiniteAutomata ParseAutomata(string textFilePath)
        {
            var lines = File.ReadLines(textFilePath);

            return ParseLines(lines.ToList());
        }

        // TODO improve code here
        private static FiniteAutomata ParseLines(List<string> lines)
        {
            var automata = new FiniteAutomata();
            for (var index = 0; index < lines.Count; index++)
            {
                var line = new string(lines[index].Where(c => !char.IsWhiteSpace(c)).ToArray());
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) continue;

                if (line.StartsWith("#"))
                {
                    automata.Comment = line.Split('#')[1];
                }
                else if (line.StartsWith("alphabet"))
                {
                    var alphabet = line.Split(':')[1];
                    automata.Alphabet = alphabet.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToList();
                }
                else if (line.StartsWith("states"))
                {
                    var allStates = line.Split(':')[1];
                    automata.States = allStates
                            .Split(',')
                            .Select(x => new State(x, new List<Transition>(), false))
                            .ToList();
                }
                else if (line.StartsWith("final"))
                {
                    var finalStates = line.Split(':')[1];
                    foreach (var finalState in finalStates.Split(','))
                    {
                        // TDO throw exception validation
                        var state = automata.States.SingleOrDefault(x => x.StateName == finalState);
                        if (state != null) state.IsFinal = true;
                    }
                }
                else if (line.StartsWith("transitions:"))
                {
                    // TODO move regex, reduce code, better regex?
                    var transitionLines = lines.GetRange(index + 1, lines.Count - index - 2).ToList();
                    var regex = new Regex("[A-Za-z0-9]*,[A-Za-z_]{1}-->[A-Za-z0-0]*");
                    foreach (var transitionLine in transitionLines)
                    {
                        var cleanTransitionLine = new string(transitionLine.Where(c => !char.IsWhiteSpace(c)).ToArray());
                        if (regex.Match(cleanTransitionLine).Success)
                        {
                            // TODO validation for states, maybe they do not exist
                            var parsedLine = cleanTransitionLine.Replace("-->", ",");
                            var elements = parsedLine.Split(',');
                            var outgoingState = automata.States.SingleOrDefault(x => x.StateName == elements[0]);
                            var transitionChar = Char.Parse(elements[1]);
                            var targetState = automata.States.SingleOrDefault(x => x.StateName == elements[2]);
                            outgoingState.Transitions.Add(new Transition(transitionChar, targetState));
                        }
                    }
                    break;
                }
            }
            // TODO calculate internally. automata should known without having to call this method explicitly.
            // maybe in c-tor
            automata.DetermineDfa();
            return automata;
        }
    }
}
