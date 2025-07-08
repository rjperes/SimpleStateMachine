namespace SimpleStateMachine.Extensions
{
    public static class StateMachineExtensions
    {
        public static IStateMachine<T> Create<T>() where T : struct, Enum
        {
            var initialState = StateMachine.GetInitialState<T>();

            ArgumentNullException.ThrowIfNull(initialState, $"The enum {typeof(T).Name} does not have an initial state.");

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
                    stateMachine.CanTransitionTo(state, transitions.ToArray());
                }
            }

            return stateMachine;
        }
    }
}
