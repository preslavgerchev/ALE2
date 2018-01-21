namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public class Word
    {
        public List<char> Letters { get; set; }

        public bool ShouldBeAccepted { get; set; }

        public bool IsAccepted { get; set; }

        public string WordString { get; }

        public Word(string word, bool shouldBeAccepted = false)
        {
            this.Letters = word.Select(x => x.ParseChar()).Where(x => x != Epsilon.Letter).ToList();
            this.WordString = new string(this.Letters.ToArray());
            this.ShouldBeAccepted = shouldBeAccepted;
        }

        public override string ToString()
        {
            return this.WordString;
        }
    }
}   
