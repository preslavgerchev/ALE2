namespace AutomataLogicEngineering2.Parser
{
    using System.Linq;
    using System.Collections.Generic;
    using Automata;

    public class StatesParser : IPartialParser<List<State>>
    {
        public List<State> Parse(List<string> lines)
        {
            var allStateNames = new List<string>();
            var finalStatesNames = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("states"))
                {
                    allStateNames = line.Split(':')[1].Split(',').ToList();
                }

                if (line.StartsWith("final"))
                {
                    finalStatesNames = line.Split(':')[1].Split(',').ToList();
                }
            }

            return allStateNames
                .Select((stateName, i) => new State(stateName, i == 0, finalStatesNames.Any(x => x == stateName)))
                .ToList();
        }
    }
}
