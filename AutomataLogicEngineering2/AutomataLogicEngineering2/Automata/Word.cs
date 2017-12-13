namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;

    public class Word
    {
        public List<char> Letters { get; set; }

        public bool ShouldBeAccepted { get; set; }

        public bool IsAccepted { get; set; }

        public string WordString { get; }

        public Word(string word, bool shouldBeAccepted = false)
        {
            this.WordString = word;
            this.Letters = word.Replace("_", string.Empty).ToCharArray().ToList();
            this.ShouldBeAccepted = shouldBeAccepted;
        }

        public override string ToString()
        {
            return this.WordString;
        }
    }
}   
