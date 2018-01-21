namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Exceptions;
    using Extensions;

    public class FiniteAutomata
    {
        public string Comment { get; }

        public Alphabet Alphabet { get; }

        public IReadOnlyList<State> States { get; }

        public bool IsDfa { get; }

        public bool IsNdfa => !this.IsDfa;

        public bool IsInfinite { get; private set; }

        public bool IsFinite => !this.IsInfinite;

        public bool ShouldBeDfa { get; }

        public bool ShouldBeFinite { get; }

        public IReadOnlyList<Word> TestWords { get; }

        public IReadOnlyList<Word> FiniteWords { get; private set; }

        public FiniteAutomata(
            string comment,
            Alphabet alphabet,
            IReadOnlyList<State> states,
            IReadOnlyList<Word> testWords,
            bool shouldBeDfa,
            bool shouldBeFinite)
        {
            this.Comment = comment;
            this.Alphabet = alphabet;
            this.States = states;
            this.TestWords = testWords;
            this.ShouldBeDfa = shouldBeDfa;
            this.ShouldBeFinite = shouldBeFinite;
            this.IsDfa = this.States.All(x => x.IsDfa(this.Alphabet));
            this.DetermineInfiniteAndWords();
        }

        public virtual bool AcceptsWord(Word word)
        {
            if (word.Letters.Any(x => !this.Alphabet.Contains(x)))
            {
                throw new InvalidCharException(
                    $"Word '{word}' is invalid as there are letters not found in the alphabet ");
            }

            var states = this.IsDfa
                ? new List<State> { this.States.GetInitialState() }
                : this.GetPossibleEpsilonStates(new List<State> { this.States.GetInitialState() });

            foreach (var letter in word.Letters)
            {
                states = this.GetPossibleStates(states, letter);
                if (this.IsDfa) continue;
                states = this.GetPossibleEpsilonStates(states);
            }
            return states.Any(x => x.IsFinal);
        }

        public void AcceptWords()
        {
            foreach (var word in this.TestWords)
            {
                word.IsAccepted = this.AcceptsWord(word);
            }
        }

        private List<State> GetPossibleStates(List<State> states, char letter)
        {
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
            var possibleStates = currentEpsilonStates.SelectMany(x => x.PossibleEpsilonStates()).Distinct().ToList();
            var newStates = possibleStates.Where(state => !currentEpsilonStates.Any(x => x.Equals(state))).ToList();

            return newStates.Any()
                ? this.GetPossibleEpsilonStates(newStates.Concat(currentEpsilonStates).ToList()).ToList()
                : currentEpsilonStates;
        }

        private void DetermineInfiniteAndWords()
        {
            // Get all possible reachable states, that is all states that can be reached from the initial one.
            var reachableStates = this.GetReachableStates();
            if (!reachableStates.Any())
            {
                this.IsInfinite = true;
                return;
            }
            if (!reachableStates.Any(x => x.IsFinal))
            {
                this.IsInfinite = true;
                return;
            }

            var terminatingStates = new List<State>();
            // Get all states that have a connection to a final state.
            var finalStates = reachableStates.GetFinalStates().ToList();
            foreach (var final in finalStates)
            {
                this.GetTerminatingStates(new List<State>(), terminatingStates, final);
            }
            terminatingStates = terminatingStates.Concat(finalStates).Distinct().ToList();
            // Starting from the initial state try to find a loop inside the terminating states.
            this.IsInfinite = terminatingStates.GetInitialState()
                .HasLoop(terminatingStates, new List<State>(), this.IsNdfa);
            // If finite, find all words.
            if (this.IsFinite) this.GetFiniteWords(terminatingStates);
        }

        private List<State> GetReachableStates()
        {
            var reachableStates = new List<State>();
            this.States.GetInitialState().GetReachableStates(reachableStates);
            return reachableStates;
        }

        private void GetTerminatingStates(List<State> visitedStates, List<State> statesThatReach, State targetEndState)
        {
            if (visitedStates.Contains(targetEndState)) return;
            visitedStates.Add(targetEndState);
            var reachableStates = this.States
                .Where(x => x.Transitions.Any(y => y.TransitionTo.Equals(targetEndState)))
                .ToList();
            statesThatReach.AddRange(reachableStates);
            foreach (var state in new List<State>(statesThatReach))
            {
                GetTerminatingStates(visitedStates, statesThatReach, state);
            }
        }

        private void GetFiniteWords(List<State> terminatingStates)
        {
            if (this.IsInfinite)
            {
                throw new Exception("Internal error. Cannot determine words for a NDFA.");
            }

            var words = new List<string>();
            terminatingStates.GetInitialState().GetWords(
                allowedStates: terminatingStates,
                visitedStates: new List<State>(),
                words: words,
                currentWordBuilder: new StringBuilder());
            this.FiniteWords = words.Distinct().Select(x => new Word(x)).ToList();
        }
    }
}
