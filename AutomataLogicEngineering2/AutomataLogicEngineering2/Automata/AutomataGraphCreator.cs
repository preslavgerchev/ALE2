namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.Diagnostics;
    using System.IO;

    // TODO documentation.
    /// <summary>
    /// A static class, responsible for creating a node graph image out of a root node.
    /// </summary>
    public static class AutomataGraphCreator
    {
        /// <summary>
        /// The file name of the .dot file that will be used to create the image.
        /// </summary>
        private const string DotFileName = "AutomataGraph.dot";

        /// <summary>
        /// The file name of the image where the node graph will be created.
        /// </summary>
        private const string ImageFileName = "AutomataGraph.png";
        
        public static string CreateNodeGraphImage(FiniteAutomata automata)
        {
            PrepareDotFile(automata);
            return CreateAutomataGraph();
        }

        /// <summary>
        /// Creates a .dot file for the given <paramref name="rootNode"/> that contains all connections in the
        /// node graph.
        /// </summary>
        /// <param name="rootNode">The root node of the node tree.</param>
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

        /// <summary>
        /// Creates the node graph image with file name of <see cref="ImageFileName"/>, using the .dot file with file name 
        /// of <see cref="DotFileName"/> and returns the full path of where the image is located.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Writes the connection between the given <paramref name="node"/> and its children, using
        /// the <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The text writer.</param>
        /// <param name="node">The node.</param>
        private static void WriteStates(TextWriter writer, FiniteAutomata automata)
        {
            foreach (var state in automata.States)
            {
                writer.WriteLine($"\"{state.StateName}\" [shape= {(state.IsFinal ? "doublecircle" : "circle")}]");
            }
        }

        /// <summary>
        /// Writes the connection between the given <paramref name="node"/> and its children, using
        /// the <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The text writer.</param>
        /// <param name="node">The node.</param>
        private static void WriteTransitions(TextWriter writer, FiniteAutomata automata)
        {
            foreach (var state in automata.States)
            {
                foreach (var transition in state.Transitions)
                {
                    writer.WriteLine(
                        $"\"{state.StateName}\" -> \"{transition.TransitionTo.StateName}\" [label = {transition.TransitionChar}]");
                }
            }
        }
    }
}
