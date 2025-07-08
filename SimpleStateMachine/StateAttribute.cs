namespace SimpleStateMachine
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class StateAttribute : Attribute
    {
        public StateAttribute(object state)
        {
            this.State = state;
        }

        public object State { get; }
    }
}
