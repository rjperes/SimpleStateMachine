namespace SimpleStateMachine
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class TransitionsAttribute<T> : Attribute
    {
        public TransitionsAttribute(params T[] transitions)
        {
            this.Transitions = transitions?.Distinct().OrderBy(x => x) ?? Enumerable.Empty<T>();
        }

        public IEnumerable<T> Transitions { get; }
    }
}
