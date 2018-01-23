namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Extensions;

    public static class AutomataGraphCreator
    {
        private const string DotFileName = "AutomataGraph.dot";
        private const string ImageFileName = "AutomataGraph.png";
        
        public static string CreateNodeGraphImage(FiniteAutomata automata)
        {
            PrepareDotFile(automata);
            return CreateAutomataGraph();
        }

        private static void PrepareDotFile(FiniteAutomata automata)
        {
            using (var writer = new StreamWriter($"../../../{DotFileName}", false))
            {
                writer.WriteLine("digraph myAutomation {");
                writer.WriteLine("rankdir=LR;");
                WriteStates(writer, automata);
                WriteTransitions(writer, automata);
                writer.WriteLine("}");
            }
        }

        private static string CreateAutomataGraph()
        {
            var relativeFolderPath = Path.GetFullPath(
                new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\")).LocalPath);
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = relativeFolderPath,
                FileName = "dot.exe",
                Arguments = $"-Tpng -o{ImageFileName} {DotFileName}"
            };
            Process.Start(startInfo)?.WaitForExit();
            return relativeFolderPath + ImageFileName;
        }

        private static void WriteStates(TextWriter writer, FiniteAutomata automata)
        {
            writer.WriteLine($"\"\" [shape=none]");
            foreach (var state in automata.States)
            {   
                writer.WriteLine($"\"{state.StateName}\" [shape= {(state.IsFinal ? "doublecircle" : "circle")}]");
            }
        }

        private static void WriteTransitions(TextWriter writer, FiniteAutomata automata)
        {
            writer.WriteLine($"\"\" -> \"{automata.States.GetInitialState().StateName}\"");
            foreach (var state in automata.States)
            {
                foreach (var transition in state.Transitions)
                {
                    writer.WriteLine(
                        $"\"{state.StateName}\" -> \"{transition.TransitionTo.StateName}\" [label = \"{transition.GetTextForGraphLabel()}\"]");
                }
            }
        }
    }
}
