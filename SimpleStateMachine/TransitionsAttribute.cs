namespace SimpleStateMachine
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class TransitionsAttribute<T>(params T[] transitions) : Attribute
    {
        public IEnumerable<T> Transitions { get; } = transitions?.Distinct().OrderBy(x => x) ?? Enumerable.Empty<T>();
    }
}
