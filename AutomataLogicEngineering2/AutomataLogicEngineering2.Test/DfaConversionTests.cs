namespace AutomataLogicEngineering2.Test
{
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DfaConversionTests
    {
        [TestMethod]
        public void FiniteTestJson()
        {
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(
                    "https://raw.githubusercontent.com/lyubomirdimov/Breaking-bad-Episode-Ale2/master/RegexToDFA.json");
                var testAutomatas = new JsonRegexToAutomata().ConvertToAutomata(json);
                foreach (var testAutomata in testAutomatas)
                {
                    var actualAutomata = RegExParser.RegExParser.GenerateAutomataForRegex(testAutomata.regex);
                    var toDfa = actualAutomata.ToDfa();
                    Assert.IsTrue(toDfa.IsDfa);
                    foreach (var testWord in toDfa.TestWords)
                    {
                        Assert.IsTrue(toDfa.AcceptsWord(testWord));
                    }
                }
            }
        }
    }
}
