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
        private readonly Regex finiteRegex = new Regex("[A-Za-z0-9]*,[A-Za-z_]{1}-->[A-Za-z0-0]*");
        private readonly Regex pdaRegex = new Regex("[A-Za-z0-9]*,[A-Za-z_]{1}\\[[a-z_]{1},[a-z_]{1}\\]-->[A-Za-z0-0]*");
        private readonly IReadOnlyList<State> states;
        private readonly Alphabet alphabet;
        private readonly bool isPda;

        public TransitionsParser(IReadOnlyList<State> states, Alphabet alphabet, bool isPda)
        {
            this.states = states;
            this.alphabet = alphabet;
            this.isPda = isPda;
        }

        public List<Transition> Parse(List<string> lines) =>
            this.isPda ? this.ParseForPda(lines) : ParseForNonPda(lines);
        
        private List<Transition> ParseForPda(List<string> lines)
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
                    if (finiteRegex.IsMatch(cleanTransitionLine))
                    {
                        var parsedLine = cleanTransitionLine.Replace("-->", ",");
                        var elements = parsedLine.Split(',');
                        var stateFrom = this.states.SingleOrDefault(x => x.StateName == elements[0]);
                        var letter = char.Parse(elements[1]).ParseChar();
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
                        transitions.Add(new PdaTransition(letter, stateFrom, stateTo, Epsilon.Letter, Epsilon.Letter));
                    }
                    else if (pdaRegex.IsMatch(cleanTransitionLine))
                    {
                        var parsedLine = cleanTransitionLine.Replace("-->", ",").Replace("[", ",").Replace("]", string.Empty);
                        var elements = parsedLine.Split(',');
                        var stateFrom = this.states.SingleOrDefault(x => x.StateName == elements[0]);
                        var letter = char.Parse(elements[1]).ParseChar();
                        var popStack = char.Parse(elements[2]).ParseChar();
                        var putStack = char.Parse(elements[3]).ParseChar();
                        var stateTo = this.states.SingleOrDefault(x => x.StateName == elements[4]);

                        if (stateTo == null)
                        {
                            throw new InvalidStateException($"State '{elements[4]}' does not exist.");
                        }
                        if (stateFrom == null)
                        {
                            throw new InvalidStateException($"State '{elements[0]}' does not exist.");
                        }
                        if (!this.alphabet.Contains(letter))
                        {
                            throw new InvalidCharException($"Letter '{letter}' does not exist in the alphabet.");
                        }
                        transitions.Add(new PdaTransition(letter, stateFrom, stateTo, popStack, putStack));
                    }
                    line = lines[++nextIndex];
                }
                break;
            }
            return transitions;
        }

        private List<Transition> ParseForNonPda(List<string> lines)
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
                    if (finiteRegex.IsMatch(cleanTransitionLine))
                    {
                        var parsedLine = cleanTransitionLine.Replace("-->", ",");
                        var elements = parsedLine.Split(',');
                        var stateFrom = this.states.SingleOrDefault(x => x.StateName == elements[0]);
                        var letter = char.Parse(elements[1]).ParseChar();
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
