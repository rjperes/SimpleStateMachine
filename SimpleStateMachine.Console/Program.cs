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
        static void AlternativeUsingAttributes()
        {
            var created = TicketState.Created;

            var initialState = StateMachine.GetInitialState<TicketState>();
            var isStateMachine = created.IsStateMachine();
            var isInitialState = created.IsInitialState();
            var isFinalState = created.IsFinalState();
            var canTransitionToClosed = created.CanTransitionTo(TicketState.Closed);
            var canTransitionToBlocked = created.CanTransitionTo(TicketState.Blocked);
            var transitions = created.GetTransitions();
        }

        static void AlternativeUsingCode()
        {
            var created = TicketState.Created;

            var stateMachine = created.Create();
            stateMachine.CanTransitionTo(created, TicketState.Ready, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.Ready, TicketState.Blocked, TicketState.InReview, TicketState.Closed, TicketState.Ready);
            stateMachine.CanTransitionTo(TicketState.InProgress, TicketState.InProgress, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.Blocked, TicketState.InProgress, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.InReview, TicketState.InProgress, TicketState.Closed);

            var initialState = stateMachine.GetInitialState();
            var isInitialState = created.IsInitialState();
            var isFinalState = created.IsFinalState();
            var canTransitionToClosed = created.CanTransitionTo(TicketState.Closed);
            var canTransitionToBlocked = created.CanTransitionTo(TicketState.Blocked);
            var transitions = created.GetTransitions();
        }

        static void Main(string[] args)
        {
            AlternativeUsingAttributes();
            AlternativeUsingCode();
        }
    }
}
