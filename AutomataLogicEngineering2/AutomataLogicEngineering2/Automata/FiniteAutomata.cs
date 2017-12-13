namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Extensions;

    public class FiniteAutomata
    {
        public string Comment { get; }

        public Alphabet Alphabet { get; }

        public IReadOnlyList<State> States { get; }

        public bool IsDfa { get; }

        public bool IsNdfa => !this.IsDfa;

        public IReadOnlyList<Word> TestWords { get; }

        public FiniteAutomata(
            string comment,
            Alphabet alphabet,
            IReadOnlyList<State> states,
            IReadOnlyList<Word> testWords)
        {
            this.Comment = comment;
            this.Alphabet = alphabet;
            this.States = states;
            this.TestWords = testWords;
            this.IsDfa = this.States.All(x => x.IsDfa(this.Alphabet));
            foreach (var word in this.TestWords)
            {
                word.IsAccepted = this.AcceptsWord(word);
            }
        }

        public bool AcceptsWord(Word word)
        {
            if (word.Letters.Any(x => !this.Alphabet.Contains(x)))
            {
                throw new InvalidCharException(
                    $"Word '{word}' is invalid as there are letters not found in the alphabet ");
            }
            var states = this.GetPossibleEpsilonStates(new List<State> { this.States.GetInitialState() });
            foreach (var letter in word.Letters)
            {
                var possibleEpsilonStates = this.GetPossibleEpsilonStates(states);
                states = this.GetPossibleStates(possibleEpsilonStates, letter);
            }
            return states.Any(x => x.IsFinal);
        }

        private List<State> GetPossibleStates(List<State> states, char letter)
        {
            // TODO list to set. also in epsilon.
            var possibleStates = new List<State>();
            foreach (var state in states)
            {
                var outgoingStates = state.Transitions
                    .Where(tr => tr.TransitionChar == letter)
                    .Select(tr => tr.TransitionTo).ToList();
                foreach (var outgoingState in outgoingStates)
                {
                    if (possibleStates.Any(x => Equals(x, outgoingState)))
                    {
                        continue;
                    }
                    possibleStates.Add(outgoingState);
                }
            }
            return possibleStates;
        }

        private List<State> GetPossibleEpsilonStates(List<State> currentEpsilonStates)
        {
            // TODO optimize to loop only for states that are new such that states are not checked multiple times.
            var possibleStates = currentEpsilonStates.SelectMany(x => x.PossibleEpsilonStates()).Distinct().ToList();
            var newStates = possibleStates.Where(state => !currentEpsilonStates.Any(x => x.Equals(state))).ToList();

            return newStates.Any()
                ? this.GetPossibleEpsilonStates(newStates.Concat(currentEpsilonStates).ToList()).ToList()
                : currentEpsilonStates;
        }
    }
}
