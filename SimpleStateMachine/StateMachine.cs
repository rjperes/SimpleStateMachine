using System.Reflection;

namespace SimpleStateMachine
{
    public static class StateMachine
    {
        public static IStateMachine<T> Create<T>(this T state) where T : struct, Enum
        {
            return new StateMachine<T>(state);
        }

        public static T? GetInitialState<T>() where T : struct, Enum
        {
            return (T?) Enum.GetNames<T>().Select(x => GetField(typeof(T), x)).SingleOrDefault(x => HasAttribute<InitialStateAttribute>(x))?.GetValue(null);
        }

        public static bool IsInitialState<T>(this T state) where T : Enum
        {
            return IsStateMachine<T>() && HasAttribute<InitialStateAttribute>(GetField(typeof(T), state.ToString()));
        }

        private static FieldInfo GetField(Type type, string name)
        {
            return type.GetField(name)!;
        }

        private static bool HasAttribute<T>(FieldInfo field) where T : Attribute
        {
            return field.GetCustomAttribute<T>() != null;
        }

        public static IEnumerable<T> GetTransitions<T>(this T state) where T : Enum
        {
            var transitions = GetField(typeof(T), state.ToString()).GetCustomAttribute<TransitionsAttribute<T>>();
            return transitions?.Transitions ?? [];
        }

        public static bool IsStateMachine<T>() where T : Enum
        {
            return IsStateMachine(typeof(T));
        }

        public static bool IsStateMachine<T>(this T state) where T : Enum
        {
            return IsStateMachine(state.GetType());
        }

        public static bool IsFinalState<T>(this T state) where T : Enum
        {
            return !HasAttribute<TransitionsAttribute<T>>(GetField(typeof(T), state.ToString()));
        }

        public static bool IsStateMachine(Type type)
        {
            return type.IsEnum && Enum.GetNames(type).Count(name => HasAttribute<InitialStateAttribute>(GetField(type, name))) == 1;
        }

        public static bool CanTransitionTo<T>(this T state, T target) where T : Enum
        {
            return IsStateMachine<T>() && GetTransitions<T>(state).Contains(target);
        }
    }
}
