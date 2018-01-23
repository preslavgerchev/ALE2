namespace AutomataLogicEngineering2.Test
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;

    [TestClass]
    public class PdaTests
    {
        [TestMethod]
        public void PdaTest_Json()
        {
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(
                    "https://raw.githubusercontent.com/lyubomirdimov/Breaking-bad-Episode-Ale2/master/PDAs.json");
                var pdas = new JsonToPda().ConvertToPda(json);
                foreach (var pda in pdas)
                {
                    foreach (var word in pda.TestWords)
                    {
                        Assert.IsTrue(pda.AcceptsWord(word));
                    }
                }
            }
        }

        [TestMethod]
        public void PdaTest1()
        {
            var lines = File.ReadAllLines("../../TestVectors/pdaTest1.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
        }

        [TestMethod]
        public void PdaTest2()
        {
            var lines = File.ReadAllLines("../../TestVectors/pdaTest2.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
        }
    }
}
