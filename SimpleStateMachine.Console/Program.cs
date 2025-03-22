namespace SimpleStateMachine.Console
{
    public enum TicketState
    {
        [InitialState]
        [Transitions<TicketState>(TicketState.Ready, TicketState.Closed)]
        Created,
        [Transitions<TicketState>(TicketState.InProgress, TicketState.Blocked, TicketState.Closed)]
        Ready,
        [Transitions<TicketState>(TicketState.Blocked, TicketState.InReview, TicketState.Closed, TicketState.Ready)]
        InProgress,
        [Transitions<TicketState>(TicketState.InProgress, TicketState.Closed)]
        Blocked,
        [Transitions<TicketState>(TicketState.InProgress, TicketState.Closed)]
        InReview,
        Closed
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var state = TicketState.Created;

            var isStateMachine = state.IsStateMachine();
            var isInitialState = state.IsInitialState();
            var isFinalState = state.IsFinalState();
            var canTransitionToClosed = state.CanTransitionTo(TicketState.Closed);
            var canTransitionToBlocked = state.CanTransitionTo(TicketState.Blocked);
            var transitions = state.GetTransitions();
        }
    }
}
