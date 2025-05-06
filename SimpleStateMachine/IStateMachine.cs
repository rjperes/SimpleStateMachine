
namespace SimpleStateMachine
{
    public interface IStateMachine<T> where T : Enum
    {
        T GetInitialState();
        IStateMachine<T> CanTransitionTo(T state, params IReadOnlyCollection<T> otherStates);
        IEnumerable<T> GetTransitions(T state);
        bool IsFinalState(T state);
        bool IsInitialState(T state);
    }

    class StateMachine<T>(T state) : IStateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, List<T>> _transitions = [];
        private readonly T _initialState = state;

        public T GetInitialState()
        {
            return _initialState;
        }

        public IStateMachine<T> CanTransitionTo(T state, params IReadOnlyCollection<T> otherStates)
        {
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
    }
}