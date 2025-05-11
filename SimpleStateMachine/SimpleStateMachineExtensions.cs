namespace SimpleStateMachine
{
    public static class SimpleStateMachineExtensions
    {
        public static IStateMachine<T> AppendTransitions<T>(this IStateMachine<T> stateMachine, T state, params T[] transitions) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);
            ArgumentNullException.ThrowIfNull(transitions);

            var currentTransitions = stateMachine.GetTransitions(state);
            var allTransitions = currentTransitions.Concat(transitions).Distinct().ToArray();

            return stateMachine.CanTransitionTo(state, allTransitions);
        }

        public static bool IsFinite<T>(this IStateMachine<T> stateMachine) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(stateMachine);

            return Enum.GetValues(typeof(T)).Cast<T>().Any(x => stateMachine.IsFinalState(x));
        }
    }
}
