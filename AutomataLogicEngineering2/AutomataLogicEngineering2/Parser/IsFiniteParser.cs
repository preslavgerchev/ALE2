namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;

    public class IsFiniteParser : IPartialParser<bool>
    {
        public bool Parse(List<string> lines)
        {
            var isDfa = false;
            foreach (var line in lines)
            {
                if (!line.StartsWith("finite")) continue;
                var splitLine = line.Split(':');
                isDfa = splitLine[1] == "y";
                break;
            }

            return isDfa;
        }
    }
}
