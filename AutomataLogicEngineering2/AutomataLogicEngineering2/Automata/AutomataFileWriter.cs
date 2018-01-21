namespace AutomataLogicEngineering2.Automata
{
    using System.IO;
    using System.Linq;
    using Utils;

    // TODO documentation.
    /// <summary>
    /// A static class, responsible for creating a node graph image out of a root node.
    /// </summary>
    public static class AutomataFileWriter
    {
        public static void WriteToFile(FiniteAutomata automata)
        {
            using (var w = File.CreateText($"../../../RegExpGenerated.txt"))
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
                            $" -> {transition.TransitionTo.StateName}");
                    }
                }
                w.WriteLine("end.");
            }
        }
    }
}
