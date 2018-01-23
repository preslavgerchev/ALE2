namespace AutomataLogicEngineering2.Test
{
    using System.Collections.Generic;
    using Utils;

    public class TestTransition
    {
        public string StartState { get; set; }
        public string EndState { get; set; }
        public string Symbol { get; set; }
    }

    public class FSM
    {
        public object Comment { get; set; }
        public List<string> States { get; set; }
        public List<string> Alphabet { get; set; }
        public string InitialState { get; set; }
        public List<string> FinalStates { get; set; }
        public List<Transition> Transitions { get; set; }
    }

    public class TestAutomata
    {
        public string regex { get; set; }
        public FSM FSM { get; set; }
        public bool IsDFA { get; set; }
        public bool IsFinite { get; set; }
        public List<string> Words { get; set; }
    }

    public class JsonRegexToAutomata
    {
        public List<TestAutomata> ConvertToAutomata(string json)
        {
            var cleanJson = json.Replace("Îµ", Epsilon.Letter.ToString());
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestAutomata>>(cleanJson);
        }
    }
}
