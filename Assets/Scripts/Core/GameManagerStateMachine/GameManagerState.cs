using UnityEngine;

public class GameManagerState : IState
{
    protected GameManagerStateMachine stateMachine;
    public GameManagerState(GameManagerStateMachine gameManagerStateMachine)
    {
        stateMachine = gameManagerStateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        
    }
}
