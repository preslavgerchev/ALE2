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
            this.FiniteWords = new List<Word>();
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
                // If DFA do not try to find epsilon closures as there are no existing ones.
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
            // Note that I have decided to implement getting states that reach final states a little bit different. I start
            // from the final state and check if there are any states that have a transition to that state. Then for that retrieved set p
            // I check if there are any states that have transition to any state in p. This goes on until either there are no more states
            // to visit or until the full set of terminating states is retrieved.
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

        public virtual FiniteAutomata ToDfa()
        {
            if (this.IsDfa) return this;

            // A dictionary to store the combined state and it's individual states.
            var currentStatesDict = new Dictionary<State, List<State>>();
            var notProcessedYetStates = new List<State>();
            var finishedStates = new List<State>();

            // First get the epsilon closure. Then make a single state and add it to the to be processed states.
            var initialEpsilonClosure = this.GetPossibleEpsilonStates(new List<State>() {this.States.GetInitialState()})
                .OrderBy(x => x.StateName)
                .ToList();
            var initialDfaState =
                this.MakeSingleState(initialEpsilonClosure, true, initialEpsilonClosure.Any(x => x.IsFinal));
            notProcessedYetStates.Add(initialDfaState);
            currentStatesDict.Add(initialDfaState, initialEpsilonClosure);
            while (notProcessedYetStates.Any())
            {
                // Always process the first one. We know that there is at least 1 because of the check in the while loop.
                var toBeProcessed = notProcessedYetStates[0];
                var currentStates = currentStatesDict[toBeProcessed];
                foreach (var letter in this.Alphabet.AlphabetChars)
                {
                    // 1. Get all the possible states reachable by the letter. If any then also get the epsilon closures that can be reached
                    // from these states. Note the ordering by name to ensure consistency and to avoid scenarios where {2,3} is considered a different
                    // state than {3,2}.
                    var states = this.GetPossibleStates(currentStates, letter);
                    if (states.Any())
                    {
                        var statesAndEpsilonClosure = this.GetPossibleEpsilonStates(states)
                            .OrderBy(x => x.StateName)
                            .ToList();
                        // Note that we mark the combined state as final as long as any of the states in the set are final.
                        var newState = this.MakeSingleState(
                            statesAndEpsilonClosure, false, statesAndEpsilonClosure.Any(x => x.IsFinal));
                        if (toBeProcessed.StateName == "B")
                        {
                            var z = false;
                        }
                        // Note that we can check if the new state is not actually the same one - in case of a self-loop.
                        newState = newState.Equals(toBeProcessed) ? toBeProcessed : newState;
                        // If the resulting state has already been processed -> assign it to the variable. Else add it to the to be processed list.
                        if (finishedStates.Contains(newState))
                        {
                            newState = finishedStates.Single(x => x.Equals(newState));
                        }
                        else if (!notProcessedYetStates.Contains(newState))
                        {
                            notProcessedYetStates.Add(newState);
                            currentStatesDict.Add(newState, statesAndEpsilonClosure);
                        }
                        toBeProcessed.AddTransition(new Transition(letter, toBeProcessed, newState));
                    }
                    // If there is no states reachable by letter that means we need a sink. First verify if one doesn't already exist. If it does, add a transition
                    // to it. Else create a new sink, make a self-transition for each letter to also ensure the sink is considered DFA and then add it directly
                    // to the finished states list.
                    else
                    {
                        var sink = finishedStates.SingleOrDefault(x => x.IsSink);
                        if (sink == null)
                        {
                            sink = new State("Sink", false, false, true);
                            foreach (var sinkLetter in this.Alphabet.AlphabetChars)
                            {
                                sink.AddTransition(new Transition(sinkLetter, sink, sink));
                            }
                            finishedStates.Add(sink);
                        }
                        toBeProcessed.AddTransition(new Transition(letter, toBeProcessed, sink));
                    }
                }
                foreach (var finished in finishedStates)
                {
                    // Make sure to update existing references to keep the transitions up to date.
                    var transitionsTo =
                        finished.Transitions.Where(x => x.TransitionTo.StateName == toBeProcessed.StateName);
                    foreach (var trans in transitionsTo)
                    {
                        trans.TransitionTo = toBeProcessed;
                    }
                }
                // Finally add the processed state to the final ones and remove it from the to be processed. Also clean up the dictionary.
                finishedStates.Add(toBeProcessed);
                notProcessedYetStates.Remove(toBeProcessed);
                currentStatesDict.Remove(toBeProcessed);
            }
            var automata = new FiniteAutomata(this.Comment, this.Alphabet, new List<State>(finishedStates), this.TestWords, true, false);
            automata.AcceptWords();
            return automata;
        }

        private State MakeSingleState(List<State> states, bool initial = false, bool isFinal = false) =>
            new State(string.Join("", states.Select(x => x.StateName)), initial, isFinal);
    }
}
