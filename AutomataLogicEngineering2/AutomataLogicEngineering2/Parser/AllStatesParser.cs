namespace AutomataLogicEngineering2.Parser
{
    using System.Linq;
    using System.Collections.Generic;
    using Automata;

    public class AllStatesParser : IPartialParser<List<State>>
    {
        public List<State> Parse(List<string> lines)
        {
            var states = new List<State>();
            foreach (var line in lines)
            {
                if (!line.StartsWith("states")) continue;

                var allStates = line.Split(':')[1];
                states = allStates
                    .Split(',')
                    .Select(x => new State(x))
                    .ToList();
                break;
            }

            return states;
        }
    }
}
