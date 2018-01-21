namespace AutomataLogicEngineering2.Automata
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Extensions;
    using Utils;

    public sealed class PdaAutomata : FiniteAutomata
    {
        public Alphabet StackAlphabet { get; }

        public PdaAutomata(
            string comment,
            Alphabet alphabet,
            IReadOnlyList<State> states,
            IReadOnlyList<Word> testWords,
            bool shouldBeDfa,
            bool shouldBeFinite,
            Alphabet stackAlphabet)
            : base(comment, alphabet, states, testWords, shouldBeDfa, shouldBeFinite)
        {
            this.StackAlphabet = stackAlphabet;
        }

        public override bool AcceptsWord(Word word)
        {
            if (word.Letters.Any(x => !this.Alphabet.Contains(x)))
            {
                throw new InvalidCharException(
                    $"Word '{word}' is invalid as there are letters not found in the alphabet ");
            }

            var currentState = this.States.GetInitialState();
            var stack = new Stack<char>();
            for (var i = 0; i < word.Letters.Count; i++)
            {
                var letter = word.Letters[i];
                var processed = this.GetPossibleState(currentState, letter, stack);
                // If the transition was an epsilon one, then decrease the index to result in processing the same letter again,
                // this time in the new state.
                if (!processed.CanMoveLetter) i--;
                if (processed.CurrentState == null)
                    return false;
                currentState = processed.CurrentState;
            }

            // After the word is finished, we can also check if there are epsilon transitions that can either move us to a final
            // state or can clean up the stack.
            var newState = this.GetPossibleEpsilonStatesWithNoPush(currentState, stack);
            while (newState != null)
            {
                currentState = newState;
                newState = this.GetPossibleEpsilonStatesWithNoPush(currentState, stack);
            }
            return !stack.Any() && currentState.IsFinal;
        }

        private (bool CanMoveLetter, State CurrentState) GetPossibleState(
            State currentState, char letter, Stack<char> stackToUse)
        {
            // First, transform all transitions to PDA transitions (containing push and pop symbols).
            var pdaTransitions = currentState.Transitions.Select(x => x as PdaTransition).ToList();
            // First case. There is matching symbol and the top symbol in the stack also 
            // matches the pop one and it is NOT epsilon.
            var possibleFirstTransition = pdaTransitions.FirstOrDefault(x =>
                x.TransitionChar == letter
                && stackToUse.Any()
                && x.PopStack == stackToUse.Peek()
                && x.PopStack != Epsilon.Letter);
            if (possibleFirstTransition != null)
            {
                this.CheckStackAlphabet(possibleFirstTransition.PopStack, possibleFirstTransition.PutStack);
                stackToUse.Pop();
                if (possibleFirstTransition.PutStack != Epsilon.Letter)
                    stackToUse.Push(possibleFirstTransition.PutStack);

                return (true, possibleFirstTransition.TransitionTo);
            }
            // Second case. There is a matching symbol and the pop one is an epsilon.
            var possibleSecondTransition = pdaTransitions.FirstOrDefault(x =>
                x.TransitionChar == letter && letter != Epsilon.Letter && x.PopStack == Epsilon.Letter);
            if (possibleSecondTransition != null)
            {
                this.CheckStackAlphabet(possibleSecondTransition.PopStack, possibleSecondTransition.PutStack);
                if (possibleSecondTransition.PutStack != Epsilon.Letter)
                    stackToUse.Push(possibleSecondTransition.PutStack);

                return (true, possibleSecondTransition.TransitionTo);
            }
            // Third case. There is no matching symbol, but an epsilon one and the top symbol 
            // in the stack also matches the pop one and it is NOT epsilon.
            var possibleThirdTransition = pdaTransitions.FirstOrDefault(x =>
                x.TransitionChar == Epsilon.Letter && stackToUse.Any() && x.PopStack == stackToUse.Peek() &&
                x.PopStack != Epsilon.Letter);
            if (possibleThirdTransition != null)
            {
                this.CheckStackAlphabet(possibleThirdTransition.PopStack, possibleThirdTransition.PutStack);
                stackToUse.Pop();
                if (possibleThirdTransition.PutStack != Epsilon.Letter)
                    stackToUse.Push(possibleThirdTransition.PutStack);

                return (false, possibleThirdTransition.TransitionTo);
            }
            // Fourth case. There is no matching symbol, but an epsilon one and the pop one is an epsilon.
            var possibleFourthTransition = pdaTransitions.FirstOrDefault(x =>
                x.TransitionChar == Epsilon.Letter && x.PopStack == Epsilon.Letter);
            if (possibleFourthTransition != null)
            {
                this.CheckStackAlphabet(possibleFourthTransition.PopStack, possibleFourthTransition.PutStack);
                if (possibleFourthTransition.PutStack != Epsilon.Letter)
                    stackToUse.Push(possibleFourthTransition.PutStack);

                return (false, possibleFourthTransition.TransitionTo);
            }
            // Else if all the cases do not match, return null to indicate that the word is not accepted.
            return (false, null);
        }

        private State GetPossibleEpsilonStatesWithNoPush(State currentState, Stack<char> stackToUse)
        {
            // Try to find epsilon transitions where either there is a pop symbol, matching the top one in the stack
            // or both pop and push are empty. These transitions can be used to move 'freely' to another state without
            // modifying the state, excepting for clearing it.
            var pdaTransitions = currentState.Transitions.Select(x => x as PdaTransition).ToList();
            var possibleEpsilonTransition = pdaTransitions.FirstOrDefault(x =>
                x.TransitionChar == Epsilon.Letter
                && ((stackToUse.Any() && x.PopStack == stackToUse.Peek() && x.PopStack != Epsilon.Letter)
                    || !stackToUse.Any() && x.PutStack == Epsilon.Letter && x.PopStack == Epsilon.Letter));
            if (possibleEpsilonTransition != null)
            {
                if (stackToUse.Any() && stackToUse.Peek() == possibleEpsilonTransition.PopStack)
                    stackToUse.Pop();
                return possibleEpsilonTransition.TransitionTo;
            }

            return null;
        }

        private void CheckStackAlphabet(char popStack, char pushStack)
        {
            if (!this.StackAlphabet.Contains(popStack))
            {
                throw new InvalidCharException(
                    $"Character {popStack} cannot be popped from the stack as it is not in the stack alphabet");
            }

            if (!this.StackAlphabet.Contains(pushStack))
            {
                throw new InvalidCharException(
                    $"Character {pushStack} cannot be popped from the stack as it is not in the stack alphabet");
            }
        }
    }
}
