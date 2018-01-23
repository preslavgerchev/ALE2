namespace AutomataLogicEngineering2.Test
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;

    [TestClass]
    public class FiniteTests
    {
        [TestMethod]
        public void FiniteTest1()
        {
            var lines = File.ReadAllLines("../../TestVectors/finiteTest1.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            Assert.IsFalse(automata.FiniteWords.Any());
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
        }

        [TestMethod]
        public void FiniteTest2()
        {
            var lines = File.ReadAllLines("../../TestVectors/finiteTest2.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            Assert.IsTrue(automata.FiniteWords.Any());
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
            Assert.IsTrue(automata.FiniteWords.ToList().Any(x => x.WordString == "xx"));
        }

        [TestMethod]
        public void FiniteTest3()
        {
            var lines = File.ReadAllLines("../../TestVectors/finiteTest3.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            Assert.IsTrue(automata.FiniteWords.Any());
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
            Assert.IsTrue(automata.FiniteWords.ToList().Any(x => x.WordString == "xxx"));
        }

        [TestMethod]
        public void FiniteTest4()
        {
            var lines = File.ReadAllLines("../../TestVectors/finiteTest4.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            Assert.IsTrue(automata.FiniteWords.Any());
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
            Assert.IsTrue(automata.FiniteWords.ToList().Any(x => x.WordString == "xxx"));
        }

        [TestMethod]
        public void FiniteTest5()
        {
            var lines = File.ReadAllLines("../../TestVectors/finiteTest5.txt").ToList();
            var automata = FiniteAutomataParser.CreateAutomata(lines);
            Assert.AreEqual(automata.ShouldBeFinite, automata.IsFinite);
            Assert.AreEqual(automata.ShouldBeDfa, automata.IsDfa);
            Assert.IsFalse(automata.FiniteWords.Any());
            foreach (var word in automata.TestWords)
            {
                Assert.AreEqual(word.ShouldBeAccepted, word.IsAccepted);
            }
        }
    }
}
