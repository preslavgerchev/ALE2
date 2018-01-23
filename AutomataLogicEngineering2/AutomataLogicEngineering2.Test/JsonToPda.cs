namespace AutomataLogicEngineering2.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Automata;
    using Utils;

    public class Transition
    {
        public string StartState { get; set; }
        public string InputSymbol { get; set; }
        public string StackTopSymbol { get; set; }
        public List<string> PushSymbols { get; set; }
        public string EndState { get; set; }
    }

    public class Pda
    {
        public List<string> InputAlphabet { get; set; }
        public List<string> StackAlphabet { get; set; }
        public List<string> States { get; set; }
        public List<Transition> Transitions { get; set; }
        public string InitialState { get; set; }
        public List<string> FinalStates { get; set; }
    }

    public class PdaTest
    {
        public Pda Pda { get; set; }
        public List<string> SomeAcceptedWords { get; set; }
    }

    public class JsonToPda
    {
        public List<PdaAutomata> ConvertToPda(string json)
        {
            var cleanJson = json.Replace("Îµ", Epsilon.Letter.ToString());
            var pdaTests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PdaTest>>(cleanJson);
            return pdaTests.Select(ConvertPdaTestToPda).ToList();
        }

        private static PdaAutomata ConvertPdaTestToPda(PdaTest pdaTest)
        {
            var states = pdaTest.Pda.States;
            var finalStates = pdaTest.Pda.FinalStates;
            var initial = pdaTest.Pda.InitialState;
            var transitions = pdaTest.Pda.Transitions;
            var pdaStates = states.Select(x => new State(x, x == initial, finalStates.Contains(x), false)).ToList();
            var pdaTransitions = transitions
                .Select(x => new PdaTransition(
                    char.Parse(x.InputSymbol),
                    pdaStates.First(y => y.StateName == x.StartState),
                    pdaStates.First(z => z.StateName == x.EndState),
                    char.Parse(x.StackTopSymbol),
                    char.Parse(x.PushSymbols[0])));
            var alphabet = new Alphabet(pdaTest.Pda.InputAlphabet.Select(char.Parse).ToList());
            var stackAlphabet = new Alphabet(pdaTest.Pda.StackAlphabet.Select(char.Parse).ToList());
            var acceptedWords = pdaTest.SomeAcceptedWords.Select(x => new Word(x, true)).ToList();
            foreach (var trans in pdaTransitions)
            {
                var initialState = pdaStates.First(x => x.Equals(trans.TransitionFrom));
                initialState.AddTransition(trans);
            }

            return new PdaAutomata(
                "Test PDA automata.", alphabet, pdaStates, acceptedWords, false, false, stackAlphabet);
        }
    }
}
