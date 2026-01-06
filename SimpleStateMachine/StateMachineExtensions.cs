namespace SimpleStateMachine
{
    public static class StateMachineExtensions
    {
        public static IState<T> From<T>(this IStateMachine<T> stateMachine, T fromState, object? state = null) where T : struct, Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);
            return new State<T>(stateMachine, fromState, state);
        }

        public static IStateMachine<T> CanTransitionFromAnyTo<T>(this IStateMachine<T> stateMachine, T finalState) where T : struct, Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);

            foreach (var state in Enum.GetValues<T>())
            {
                if (state!.Equals(finalState))
                {
                    continue;
                }

                stateMachine.CanTransitionTo(finalState, state);
            }
            
            return stateMachine;
        }

        public static bool IsValidTransition<T>(this IStateMachine<T> stateMachine, T fromState, T toState) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);
            var transitions = stateMachine.GetTransitions(fromState);
            return transitions.Contains(toState);
        }

        public static IStateMachine<T> Create<T>() where T : struct, Enum
        {
            var initialState = StateMachine.GetInitialState<T>();

            ArgumentNullException.ThrowIfNull(initialState);

            var stateMachine = initialState.Value.Create();

            foreach (var state in Enum.GetValues(typeof(T)).Cast<T>())
            {
                var transitions = state.GetTransitions();
                if (transitions.Any())
                {
                    var someState = state.GetState();
                    if (someState != null)
                    {
                        stateMachine.SetState(state, someState);
                    }
                    stateMachine.CanTransitionTo(state, [.. transitions]);
                }
            }

            return stateMachine;
        }

        public static IStateMachine<T> AppendTransitions<T>(this IStateMachine<T> stateMachine, T state, params T[] transitions) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);
            ArgumentNullException.ThrowIfNull(transitions);

            T[] allTransitions = [];

            if (transitions.Length != 0)
            {
                var currentTransitions = stateMachine.GetTransitions(state);
                allTransitions = currentTransitions.Concat(transitions).Distinct().ToArray();
            }

            return stateMachine.CanTransitionTo(state, allTransitions);
        }

        public static bool IsFinite<T>(this IStateMachine<T> stateMachine) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);

            return Enum.GetValues(typeof(T)).Cast<T>().Any(x => stateMachine.IsFinalState(x));
        }
    }
}
