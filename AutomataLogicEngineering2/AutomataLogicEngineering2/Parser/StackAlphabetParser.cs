namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using Automata;

    public class StackAlphabetParser : IPartialParser<Alphabet>
    {
        public Alphabet Parse(List<string> lines)
        {
            foreach (var line in lines)
            {
                if (!line.StartsWith("stack")) continue;
                var stack = line.Split(':')[1].ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToList();
                return new Alphabet(stack);
            }

            // TODO throw?
            return null;
        }
    }
}
