namespace AutomataLogicEngineering2.RegExParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Automata;
    using Extensions;
    using Symbols;

    public sealed class Node
    {
        private static int idGenerator;

        public Symbol Symbol { get; }

        public Node Parent { private set; get; }

        public List<Node> Children { get; }

        public Node(Symbol symbol)
        {
            this.Symbol = symbol;
            this.Children = new List<Node>();
        }

        public void AddChild(Node node)
        {
            this.Children.Add(node);
            node.Parent = this;
        }

        public List<State> Apply()
        {
            if (this.Symbol is Predicate)
            {
                return this.GenerateForSingle();
            }

            if (!(this.Symbol is Operator))
            {
                throw new Exception($"Internal error. Cannot call Apply(..) for symbol '{this.Symbol.CharSymbol}'.");
            }

            var currentOperator = (Operator) this.Symbol;

            return this.PrivateApply(
                this.Children[0].Apply(),
                currentOperator.Type == OperatorType.KleeneStar ? null : this.Children[1].Apply(),
                currentOperator.Type);
        }

        private List<State> GenerateForSingle()
        {
            var state = new State($"A{idGenerator++}", true);
            var endState = new State($"A{idGenerator++}", false, true);
            state.AddTransitions(this.Symbol.CharSymbol, endState);
            return new List<State>() { state, endState };
        }

        private List<State> PrivateApply(
            List<State> firstOperatorStates, 
            List<State> secondOperatorStates,
            OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Concatenation:
                    return this.ConcatenationStates(firstOperatorStates, secondOperatorStates);
                case OperatorType.Union:
                    return this.UnionStates(firstOperatorStates, secondOperatorStates);
                case OperatorType.KleeneStar:
                    return this.KleeneStarStates(firstOperatorStates);
                default:
                    throw new Exception($"Unknown type {type} found.");
            }
        }

        private List<State> UnionStates(List<State> firstOperatorStates, List<State> secondOperatorStates)
        {
            var initialState = new State($"A{idGenerator++}", true);
            var finalState = new State($"A{idGenerator++}", false, true);
            var existingFirstInitial = firstOperatorStates.GetInitialState();
            var existingFirstFinal = firstOperatorStates.GetFinalStates()[0];
            var existingSecondInitial = secondOperatorStates.GetInitialState();
            var existingSecondFinal = secondOperatorStates.GetFinalStates()[0];

            existingFirstInitial.IsInitial = false;
            existingSecondInitial.IsInitial = false;
            existingFirstFinal.IsFinal = false;
            existingSecondFinal.IsFinal = false;
            initialState.AddTransitions(existingFirstInitial, existingSecondInitial);
            existingFirstFinal.AddTransitions(finalState);
            existingSecondFinal.AddTransitions(finalState);
            return new List<State> {initialState, finalState}
                .Concat(firstOperatorStates)
                .Concat(secondOperatorStates)
                .ToList();
        }

        private List<State> KleeneStarStates(List<State> firstOperatorStates)
        {
            var initialState = new State($"A{idGenerator++}", true);
            var finalState = new State($"A{idGenerator++}", false, true);
            var currentInitialState = firstOperatorStates.GetInitialState();
            var currentFinalState = firstOperatorStates.GetFinalStates()[0];
            currentInitialState.IsInitial = false;
            currentFinalState.IsFinal = false;

            currentFinalState.AddTransitions(currentInitialState, finalState);
            initialState.AddTransitions(currentInitialState, finalState);
            return new List<State> {initialState, finalState}.Concat(firstOperatorStates).ToList();
        }

        private List<State> ConcatenationStates(List<State> firstOperatorStates, List<State> secondOperatorStates)
        {
            var firstFinalState = firstOperatorStates.GetFinalStates()[0];
            var secondStartState = secondOperatorStates.GetInitialState();
            secondStartState.IsInitial = false;
            firstFinalState.IsFinal = false;

            firstFinalState.AddTransitions(secondStartState);
            return firstOperatorStates.Concat(secondOperatorStates).ToList();
        }
    }
}