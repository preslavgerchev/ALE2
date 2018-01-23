namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.IO;
    using System.Linq;
    using Utils;

    public static class AutomataFileWriter
    {
        public static void WriteToFile(FiniteAutomata automata,string fileName)
        {
            using (var w = File.CreateText($"../../../{fileName}.txt"))
            {
                w.WriteLine($"# {automata.Comment}");
                w.WriteLine(
                    $"alphabet: {string.Join(",", automata.Alphabet.AlphabetChars.Select(char.ToLower).ToList())}");
                w.WriteLine(
                    $"states: {string.Join(",", automata.States.OrderByDescending(x => x.IsInitial).Select(x => x.StateName))}");
                w.WriteLine(
                    $"final: {string.Join(",", automata.States.Where(x => x.IsFinal).Select(x => x.StateName))}");
                w.WriteLine("transitions:");
                foreach (var state in automata.States)
                {
                    foreach (var transition in state.Transitions)
                    {
                        w.WriteLine(
                            $"{transition.TransitionFrom.StateName}," +
                            $"{(transition.TransitionChar == Epsilon.Letter ? '_' : transition.TransitionChar)}" +
                            $" --> {transition.TransitionTo.StateName}");
                    }
                }
                w.WriteLine("end.");
                w.WriteLine(Environment.NewLine);

                if (automata.TestWords.Any())
                {
                    w.WriteLine("words:");
                    foreach (var word in automata.TestWords)
                    {
                        w.WriteLine($"{word},{(word.IsAccepted ? "y" : "n")}");
                    }
                    w.WriteLine("end.");
                }
                w.WriteLine(Environment.NewLine);
                w.WriteLine($"finite: {(automata.IsFinite ? "y" : "n")}");
                w.WriteLine(Environment.NewLine);
                w.WriteLine($"dfa: {(automata.IsDfa ? "y" : "n")}");
            }
        }
    }
}
