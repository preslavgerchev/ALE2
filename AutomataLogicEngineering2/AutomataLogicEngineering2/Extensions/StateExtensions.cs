namespace AutomataLogicEngineering2.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Automata;
    using Exceptions;

    public static class StateExtensions
    {
        public static State GetInitialState(this IEnumerable<State> states)
        {
            var initialState = states.SingleOrDefault(x => x.IsInitial);
            return initialState ?? throw new InvalidStateException("No state in the automata is marked as initial.");
        }
    }
}
