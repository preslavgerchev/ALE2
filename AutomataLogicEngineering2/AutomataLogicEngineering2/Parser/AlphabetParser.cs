namespace AutomataLogicEngineering2.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Automata;

    public class AlphabetParser : IPartialParser<Alphabet>
    {
        public Alphabet Parse(List<string> lines)
        {
            var alphabet = new List<char>();
            foreach (var line in lines)
            {
                if (line.StartsWith("alphabet"))
                {
                    alphabet = line.Split(':')[1].ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToList();
                    break;
                }
            }

            return alphabet.Any() ? new Alphabet(alphabet) : throw new Exception("Cannot have an empty alphabet.");
        }
    }
}
