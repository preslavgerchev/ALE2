namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Automata;
    using Exceptions;
    using Utils;

    public class TransitionsParser : IPartialParser<List<Transition>>
    {
        // TODO underscores
        private readonly Regex regex = new Regex("[A-Za-z0-9]*,[A-Za-z_]{1}-->[A-Za-z0-0]*");
        private readonly IReadOnlyList<State> states;
        private readonly Alphabet alphabet;

        public TransitionsParser(IReadOnlyList<State> states, Alphabet alphabet)
        {
            this.states = states;
            this.alphabet = alphabet;
        }

        public List<Transition> Parse(List<string> lines)
        {
            var transitions = new List<Transition>();
            for (var index = 0; index < lines.Count; index++)
            {
                var line = lines[index];
                if (!line.StartsWith("transitions:")) continue;
                var nextIndex = index + 1;
                line = lines[nextIndex];
                while (line != "end.")
                {
                    var cleanTransitionLine = new string(line.Where(c => !char.IsWhiteSpace(c)).ToArray());
                    if (regex.Match(cleanTransitionLine).Success)
                    {
                        var parsedLine = cleanTransitionLine.Replace("-->", ",");
                        var elements = parsedLine.Split(',');
                        var stateFrom = this.states.SingleOrDefault(x => x.StateName == elements[0]);
                        var letter = char.Parse(elements[1]);
                        letter = letter == '_' ? Epsilon.Letter : letter;
                        var stateTo = this.states.SingleOrDefault(x => x.StateName == elements[2]);

                        if (stateTo == null)
                        {
                            throw new InvalidStateException($"State '{elements[2]}' does not exist.");
                        }
                        if (stateFrom == null)
                        {
                            throw new InvalidStateException($"State '{elements[0]}' does not exist.");
                        }
                        if (!this.alphabet.Contains(letter))
                        {
                            throw new InvalidCharException($"Letter '{letter}' does not exist in the alphabet.");
                        }
                        transitions.Add(new Transition(letter, stateFrom, stateTo));
                    }
                    line = lines[++nextIndex];
                }
                break;
            }
            return transitions;
        }
    }
}
