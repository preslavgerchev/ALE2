namespace AutomataLogicEngineering2.Test
{
    using System.IO;
    using System.Linq;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;

    [TestClass]
    public class AutomataTests
    {
        [TestMethod]
        public void AutomataParsing_Test1()
        {
            var automataLines = File.ReadAllLines("../../../testVectors.txt")
                .Split(x => x.StartsWith("-"))
                .ToList();
            foreach (var automata1 in automataLines)
            {
                var automata = FiniteAutomataParser.CreateAutomata(automata1.ToList());
                Assert.AreEqual(3, automata.States.Count);
                //Assert.AreEqual("first automata", automata.Comment);
            }
        }
    }
}
