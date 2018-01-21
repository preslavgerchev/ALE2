namespace AutomataLogicEngineering2.Automata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utils;

    public class State : IEquatable<State>
    {
        public string StateName { get; }

        public IList<Transition> Transitions { get; }

        public bool IsFinal { get; set; }

        public bool IsInitial { get; set; }

        public bool IsSink { get; set; }

        public State(string stateName, bool isInitial = false, bool isFinal = false, bool isSink = false)
        {
            this.StateName = stateName;
            this.IsInitial = isInitial;
            this.IsFinal = isFinal;
            this.IsSink = isSink;
            this.Transitions = new List<Transition>();
        }

        public bool Equals(State other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(StateName, other.StateName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((State)obj);
        }

        public override int GetHashCode()
        {
            return (this.StateName != null ? this.StateName.GetHashCode() : 0);
        }

        public List<State> PossibleEpsilonStates()
        {
            var epsilonTransitions = this.Transitions.Where(x => x.IsEpsilon).ToList();
            return epsilonTransitions.Select(x => x.TransitionTo).ToList();
        }

        public bool IsDfa(Alphabet alphabet)
        {
            if (this.Transitions.Any(x => x.IsEpsilon)) return false;
            if (this.Transitions.Count != alphabet.AlphabetChars.Count) return false;

            return alphabet.AlphabetChars.All(ch => this.Transitions.Any(tr => tr.TransitionChar == ch));
        }

        public void AddTransitions(params State[] statesTo) => this.AddTransitions(Epsilon.Letter, statesTo);

        public void AddTransitions(char transitionChar, params State[] statesTo)
        {
            foreach (var state in statesTo)
            {
                this.Transitions.Add(new Transition(transitionChar, this, state));
            }
        }

        public bool HasLoop(List<State> allowedStates, List<State> visitedStates, bool isNdfa)
        {
            foreach (var trans in this.Transitions.Where(x => allowedStates.Contains(x.TransitionTo)).ToList())
            {
                // If there is a transition back to an already visited state, then find the state where the loop ends.
                // Then for the set {A,q,C} where A is the found state (where the loop starts and ends) and where C is the
                // current state an B is q is the set of states in between try to find at least 1 transition that is not
                // epsilon leading from A to any state in q, from any state in q to another state in q, or finally from the
                // last state in q to C.
                if (visitedStates.Any(x => this.Transitions.Any(y => y.TransitionTo.Equals(x))))
                {
                    var initial = visitedStates.First(x => this.Transitions.Any(y => y.TransitionTo.Equals(x)));
                    var index = visitedStates.IndexOf(initial);
                    var statesInBetween = visitedStates.GetRange(index, visitedStates.Count - index);
                    statesInBetween.Add(this);
                    for (var i = 0; i < statesInBetween.Count; i++)
                    {
                        var current = statesInBetween[i];
                        var toLookAt = i == statesInBetween.Count - 1 ? statesInBetween[0] : statesInBetween[i + 1];
                        if (current.Transitions.Where(x => x.TransitionTo.Equals(toLookAt)).Any(x => !x.IsEpsilon))
                        {
                            return true;
                        }
                    }
                }
                // Else if there is a transition to the same state verify if it is not an epsilon one.
                else if (this.Transitions.Any(x => Equals(x.TransitionTo, this) && !x.IsEpsilon)) return true;
                if (!visitedStates.Contains(trans.TransitionTo))
                {
                    var newStates = new List<State>(visitedStates) {this};
                    var hasLoop = trans.TransitionTo.HasLoop(allowedStates, newStates, isNdfa);
                    if (hasLoop)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void GetReachableStates(List<State> reachableStates)
        {
            if (reachableStates.Contains(this))
            {
                return;
            }
            reachableStates.Add(this);
            foreach (var trans in this.Transitions)
            {
                trans.TransitionTo.GetReachableStates(reachableStates);
            }
        }

        public void GetWords(
            List<State> allowedStates,
            List<State> visitedStates,
            List<string> words,
            StringBuilder currentWordBuilder)
        {
            if (visitedStates.Contains(this))
            {
                return;
            }
            visitedStates.Add(this);
            // Filter the transitions that are only in the allowedStates. These are equal to the retrieved terminating states.
            foreach (var trans in this.Transitions.Where(x => allowedStates.Contains(x.TransitionTo)).ToList())
            {
                var newBuilder = new StringBuilder(currentWordBuilder.ToString());
                if (!trans.IsEpsilon) newBuilder.Append(trans.TransitionChar);

                // If the transition leads to a final state, add the built word to the words list.
                if (trans.TransitionTo.IsFinal)
                {
                    words.Add(newBuilder.ToString());
                }
                trans.TransitionTo.GetWords(
                    allowedStates,
                    // Use a new list to not share visited states between different states as this could cause issues.
                    new List<State>(visitedStates),
                    words,
                    newBuilder);
            }
        }
    }
}
