namespace AutomataLogicEngineering2.Test
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Automata;

    [TestClass]
    public class RegexToAutomataTests
    {
        [TestMethod]
        public void RegexToAutomataTestsJson()
        {
            var json = File.ReadAllText("../../TestVectors/regexToAutomataJson.txt");
            var testAutomatas = new JsonRegexToAutomata().ConvertToAutomata(json);
            foreach (var testAutomata in testAutomatas)
            {
                var actualAutomata = RegExParser.RegExParser.GenerateAutomataForRegex(testAutomata.regex);
                Assert.AreEqual(testAutomata.IsFinite, actualAutomata.IsFinite, $"Failed for {testAutomata.regex}");
                Assert.AreEqual(testAutomata.IsDFA, actualAutomata.IsDfa, $"Failed for {testAutomata.regex}");
                CollectionAssert.AreEqual(
                    testAutomata.FSM.Alphabet.Select(char.Parse).OrderBy(x => x).ToList(),
                    actualAutomata.Alphabet.AlphabetChars.OrderBy(x => x).ToList());
                if (actualAutomata.IsFinite)
                {
                    foreach (var word in testAutomata.Words)
                    {
                        Assert.IsTrue(actualAutomata.AcceptsWord(new Word(word)));
                    }
                }
            }
        }
    }
}
