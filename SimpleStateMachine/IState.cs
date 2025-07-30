namespace SimpleStateMachine
{
    public interface IState<T> where T : struct, Enum
    {
        IState<T> To(params T[] states);
    }

    class State<T> : IState<T> where T : struct, Enum
    {
        private readonly IStateMachine<T> _stateMachine;
        private readonly T _fromState;
        private readonly object? _state;

        public State(IStateMachine<T> stateMachine, T fromState, object? state)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _fromState = fromState;
            _state = state;

            if (state != null)
            {
                _stateMachine.SetState(fromState, state);
            }
        }

        public IState<T> To(params T[] states)
        {
            ArgumentNullException.ThrowIfNull(states);
            _stateMachine.AppendTransitions(_fromState, states);
            return this;
        }
    }
}
