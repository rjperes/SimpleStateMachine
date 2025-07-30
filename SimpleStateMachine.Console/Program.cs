namespace SimpleStateMachine.Console
{
    public enum TicketState
    {
        [InitialState]
        [State("Initial State")]
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
            var state = initialState!.Value.GetState();
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
            stateMachine.SetState(created, "Initial State");
            var state = stateMachine.GetState(created);
            stateMachine.CanTransitionTo(created, TicketState.Ready, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.Ready, TicketState.Blocked, TicketState.InReview, TicketState.Closed, TicketState.Ready);
            stateMachine.CanTransitionTo(TicketState.InProgress, TicketState.InProgress, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.Blocked, TicketState.InProgress, TicketState.Closed);
            stateMachine.CanTransitionTo(TicketState.InReview, TicketState.InProgress, TicketState.Closed);

            var initialState = stateMachine.GetInitialState();
            var isInitialState = stateMachine.IsInitialState(created);
            var isFinalState = stateMachine.IsFinalState(created);
            var canTransitionToClosed = stateMachine.CanTransitionTo(created, TicketState.Closed);
            var canTransitionToBlocked = stateMachine.CanTransitionTo(created, TicketState.Blocked);
            var transitions = stateMachine.GetTransitions(created);
        }

        static void ConvertFromAttributesToCode()
        {
            var created = TicketState.Created;
            var stateMachine = SimpleStateMachineExtensions.Create<TicketState>();

            var initialState = stateMachine.GetInitialState();
            var state = stateMachine.GetState(created);
            var isInitialState = stateMachine.IsInitialState(created);
            var isFinalState = stateMachine.IsFinalState(created);
            var canTransitionToClosed = stateMachine.CanTransitionTo(created, TicketState.Closed);
            var canTransitionToBlocked = stateMachine.CanTransitionTo(created, TicketState.Blocked);
            var transitions = stateMachine.GetTransitions(created);
        }

        static void AlternativeUsingLoquacious()
        {
            var created = TicketState.Created;
            var stateMachine = SimpleStateMachineExtensions.Create<TicketState>();

            stateMachine
                .From(created)
                .To(TicketState.Ready, TicketState.Closed);
            stateMachine
                .From(TicketState.Ready)
                .To(TicketState.InProgress, TicketState.Blocked, TicketState.Closed);
            stateMachine
                .From(TicketState.InProgress)
                .To(TicketState.Blocked, TicketState.InReview, TicketState.Closed, TicketState.Ready);
            stateMachine
                .From(TicketState.Blocked)
                .To(TicketState.InProgress, TicketState.Closed);
            stateMachine
                .From(TicketState.InReview)
                .To(TicketState.InProgress, TicketState.Closed);

            var initialState = stateMachine.GetInitialState();
            var state = stateMachine.GetState(created);
            var isInitialState = stateMachine.IsInitialState(created);
            var isFinalState = stateMachine.IsFinalState(created);
            var transitions = stateMachine.GetTransitions(created);
        }

        static void Main()
        {
            AlternativeUsingAttributes();
            AlternativeUsingCode();
            AlternativeUsingLoquacious();
            ConvertFromAttributesToCode();
        }
    }
}
