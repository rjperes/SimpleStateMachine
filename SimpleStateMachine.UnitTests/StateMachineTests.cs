namespace SimpleStateMachine.UnitTests
{
    enum TaskState
    {
        Created,
        InProgress,
        Completed
    }

    public class StateMachineTests
    {
        private static readonly IStateMachine<TaskState> _stateMachine = TaskState.Created.Create();

        public StateMachineTests()
        {
            _stateMachine.CanTransitionTo(state: TaskState.Created, otherStates: [ TaskState.Completed, TaskState.InProgress ]);
            _stateMachine.CanTransitionTo(state: TaskState.InProgress, otherStates: TaskState.Completed);
        }

        [Fact]
        public void CanGetInitialState()
        {
            Assert.Equal(TaskState.Created, _stateMachine.GetInitialState());
        }

        [Fact]
        public void CanGetFinalStates()
        {
            Assert.True(_stateMachine.IsFinalState(TaskState.Completed));
            Assert.False(_stateMachine.IsFinalState(TaskState.Created));
        }

        [Fact]
        public void CanGetTransitions()
        {
            var createdTransitions = _stateMachine.GetTransitions(TaskState.Created);
            var completedTransitions = _stateMachine.GetTransitions(TaskState.Completed);
            var inProgressTransitions = _stateMachine.GetTransitions(TaskState.InProgress);

            Assert.Equal([TaskState.Completed, TaskState.InProgress], createdTransitions);
            Assert.Equal([], completedTransitions);
            Assert.Equal([TaskState.Completed], inProgressTransitions);
        }

        [Fact]
        public void CanTransitionToOtherState()
        {
            Assert.True(_stateMachine.IsValidTransition(fromState: TaskState.Created, toState: TaskState.InProgress));
            Assert.True(_stateMachine.IsValidTransition(fromState: TaskState.Created, toState: TaskState.InProgress));
            Assert.True(_stateMachine.IsValidTransition(fromState: TaskState.Created, toState: TaskState.Completed));
            Assert.False(_stateMachine.IsValidTransition(fromState: TaskState.Completed, toState: TaskState.Created));
            Assert.False(_stateMachine.IsValidTransition(fromState: TaskState.Created, toState: TaskState.Created));
        }
    }
}
