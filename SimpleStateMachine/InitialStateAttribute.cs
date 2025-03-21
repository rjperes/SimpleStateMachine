namespace SimpleStateMachine
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class InitialStateAttribute : Attribute
    {
    }
}
