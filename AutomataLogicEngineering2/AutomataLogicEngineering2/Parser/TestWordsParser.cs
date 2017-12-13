using AutomataLogicEngineering2.Utils;

namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;
    using Automata;

    public class TestWordsParser : IPartialParser<List<Word>>
    {
        public List<Word> Parse(List<string> lines)
        {
            var words = new List<Word>();
            for (var index = 0; index < lines.Count; index++)
            {
                var line = lines[index];
                if (!line.StartsWith("words:")) continue;
                var nextIndex = index + 1;
                line = lines[nextIndex];
                while (line != "end.")
                {
                    var elements = line.Split(',');
                    var word = elements[0];
                    var shouldBeAccepted = elements[1] == "y";
                    words.Add(new Word(word, shouldBeAccepted));
                    nextIndex++;
                    line = lines[nextIndex];
                }
                break;
            }
            return words;
        }
    }
}
