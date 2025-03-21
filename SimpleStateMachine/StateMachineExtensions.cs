using System.Reflection;

namespace SimpleStateMachine
{
    public static class StateMachineExtensions
    {
        public static bool IsInitialState<T>(this T state) where T : Enum
        {
            return IsStateMachine<T>() && HasAttribute<InitialStateAttribute>(GetField(typeof(T), state.ToString()));
        }

        private static FieldInfo GetField(Type type, string name)
        {
            return type.GetField(name);
        }

        private static bool HasAttribute<T>(FieldInfo field) where T : Attribute
        {
            return field.GetCustomAttribute<T>() != null;
        }

        private static IEnumerable<T> GetTransitions<T>(this T state) where T : Enum
        {
            var transitions = GetField(typeof(T), state.ToString()).GetCustomAttribute<TransitionsAttribute<T>>();
            return transitions.Transitions ?? Enumerable.Empty<T>();
        }

        public static bool IsStateMachine<T>() where T : Enum
        {
            return IsStateMachine(typeof(T));
        }

        public static bool IsStateMachine<T>(this T state) where T : Enum
        {
            return IsStateMachine(typeof(T));
        }

        public static bool IsFinalState<T>(this T state) where T : Enum
        {
            return !HasAttribute<TransitionsAttribute<T>>(GetField(typeof(T), state.ToString()));
        }

        public static bool IsStateMachine(Type type)
        {
            return type.IsEnum && Enum.GetNames(type).Any(name => HasAttribute<InitialStateAttribute>(GetField(type, name)));
        }

        public static bool CanTransitionTo<T>(this T state, T target) where T : Enum
        {
            return IsStateMachine<T>() && GetTransitions<T>(state).Contains(target);
        }
    }
}
