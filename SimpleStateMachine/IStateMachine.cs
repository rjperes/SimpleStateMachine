
namespace SimpleStateMachine
{
    public interface IStateMachine<T> where T : Enum
    {
        T GetInitialState();
        IStateMachine<T> CanTransitionTo(T state, params T[] otherStates);
        IEnumerable<T> GetTransitions(T state);
        bool IsFinalState(T state);
        bool IsInitialState(T state);
        void SetState(T state, object someState);
        object? GetState(T state);
    }

    class StateMachine<T>(T state) : IStateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, List<T>> _transitions = [];
        private readonly Dictionary<T, object> _states = [];
        private readonly T _initialState = state;

        public T GetInitialState()
        {
            return _initialState;
        }

        public IStateMachine<T> CanTransitionTo(T state, params T[] otherStates)
        {
            ArgumentNullException.ThrowIfNull(otherStates);

            if (otherStates.Length == 0)
            {
                _transitions.Remove(state);
                return this;
            }

            otherStates = otherStates ?? [];
            _transitions[state] = [.. otherStates];
            return this;
        }

        public IEnumerable<T> GetTransitions(T state)
        {
            if (_transitions.TryGetValue(state, out var transitions))
            {
                return transitions;
            }
            return [];
        }

        public bool IsFinalState(T state)
        {
            if (_transitions.TryGetValue(state, out var transitions))
            {
                return transitions.Count == 0;
            }
            return false;
        }

        public bool IsInitialState(T state)
        {
            return object.Equals(_initialState, state);
        }

        public void SetState(T state, object someState)
        {
            _states[state] = someState;
        }

        public object? GetState(T state)
        {
            if (_states.TryGetValue(state, out var someState))
            {
                return someState;
            }
            return null;
        }
    }
}
