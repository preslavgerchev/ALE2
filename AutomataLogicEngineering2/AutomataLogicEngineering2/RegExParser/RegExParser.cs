namespace AutomataLogicEngineering2.RegExParser
{
    using System.Collections.Generic;
    using Automata;

    /// <summary>
    /// A static class, responsible for parsing a given string input in a list of symbols.
    /// </summary>
    public static class RegExParser
    {
        public static FiniteAutomata GenerateAutomataForRegex(string input)
        {
            var rootNode = NodeTreeCreator.Initialize(input);
            var states = rootNode.Apply();
            var alphabet = new Alphabet(input.ToCharArray());
            var automata = new FiniteAutomata(
                $"Automata for regular expression {input}",
                alphabet,
                states,
                new List<Word>(),
                false,
                false);
            automata.AcceptWords();
            return automata;
        }
    }
}
