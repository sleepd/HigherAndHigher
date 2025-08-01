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
        Debug.Log($"[FSM] Entering {this.GetType().Name}");
    }

    public virtual void Exit()
    {
        Debug.Log($"[FSM] Exiting {this.GetType().Name}");
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
