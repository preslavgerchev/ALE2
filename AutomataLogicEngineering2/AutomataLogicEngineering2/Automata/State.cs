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

        public void AddTransition(Transition transition) => this.Transitions.Add(transition);

        public bool HasLoop(List<State> allowedStates, List<State> visitedStates, bool isNdfa)
        {
            foreach (var trans in this.Transitions.Where(x => allowedStates.Contains(x.TransitionTo)).ToList())
            {
                // Find possible loops, that is any states that have already been visited.
                var possibleLoops = visitedStates
                    .Where(x => this.Transitions.Any(y => y.TransitionTo.Equals(x)))
                    .ToList();

                // If there is a transition back to an already visited state, then find the state where the loop ends.
                // Then for the set {A,n..,C} where A is the found state (where the loop starts and ends) and where C is the
                // current state and n.. is the set of states in between A and C try to find at least 1 transition that is not
                // epsilon leading from A to any state in q, from any state in q to another state in q, or finally from the
                // last state in q to C.
                foreach (var possibleLoop in possibleLoops)
                {
                    var index = visitedStates.IndexOf(possibleLoop);
                    var statesInBetween = new List<State>(visitedStates.GetRange(index, visitedStates.Count - index)) { this };
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
                if (this.Transitions.Any(x => Equals(x.TransitionTo, this) && !x.IsEpsilon)) return true;
                if (!visitedStates.Contains(trans.TransitionTo))
                {
                    var newStates = new List<State>(visitedStates) { this };
                    var hasLoop = trans.TransitionTo.HasLoop(allowedStates, newStates, isNdfa);
                    // Make sure to only exit the recursive function if one transition has returned true. Else if we simply return no matter the value
                    // we will always exit upon checking the first transition and if the first one has no loop we will have not verified
                    // fully all transitions in the list.
                    if (hasLoop)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Find all reachable states given a list of already reached states. Will simply call itself recursively until there are no more states
        // to visit or until the full set of reachable states is retrieved.
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
            // If we have reached a final state, add the word that has been building up until now.
            if (this.IsFinal)
            {
                words.Add(currentWordBuilder.ToString());
            }
            // Filter the transitions that are only in the allowedStates. These are equal to the retrieved terminating states.
            foreach (var trans in this.Transitions.Where(x => allowedStates.Contains(x.TransitionTo)).ToList())
            {
                var newBuilder = new StringBuilder(currentWordBuilder.ToString());
                if (!trans.IsEpsilon) newBuilder.Append(trans.TransitionChar);
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
