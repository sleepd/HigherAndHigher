public abstract class StateMachine
{
    protected IState currentState;
    protected IState previousState;

    public IState PreviousState { get => previousState; }

    public void ChangeState(IState newState)
    {
        previousState = currentState;
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void HandleInput()
    {
        currentState.HandleInput();
    }

     public void Update()
    {
        currentState.Update();
    }

     public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }


}
