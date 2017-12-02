namespace AutomataLogicEngineering2.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Automata;

    public class TransitionsParser : IPartialParser<List<Transition>>
    {
        public IReadOnlyList<State> States { get; }
        public TransitionsParser(IReadOnlyList<State> states)
        {
            this.States = states;
        }

        public List<Transition> Parse(List<string> lines)
        {
            var transitions = new List<Transition>();
            for (var index = 0; index < lines.Count; index++)
            {
                var line = lines[index];
                if(!line.StartsWith("transitions:")) continue;

                // TODO move regex, reduce code, better regex?
                var transitionLines = lines.GetRange(index + 1, lines.Count - index - 2).ToList();
                var regex = new Regex("[A-Za-z0-9]*,[A-Za-z_]{1}-->[A-Za-z0-0]*");
                foreach (var transitionLine in transitionLines)
                {
                    var cleanTransitionLine = new string(transitionLine.Where(c => !char.IsWhiteSpace(c)).ToArray());
                    if (regex.Match(cleanTransitionLine).Success)
                    {
                        var parsedLine = cleanTransitionLine.Replace("-->", ",");
                        var elements = parsedLine.Split(',');
                        var stateFrom = this.States.SingleOrDefault(x => x.StateName == elements[0]);
                        var transitionChar = Char.Parse(elements[1]);
                        var stateTo = this.States.SingleOrDefault(x => x.StateName == elements[2]);
                        transitions.Add(new Transition(transitionChar, stateFrom, stateTo));
                    }
                }
                break;
            }

            return transitions;
        }
    }
}
