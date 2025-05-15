using System;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected Vector2 movementInput;
    protected PlayerController playerController;
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
        playerController = stateMachine.PlayerController;

    }

    #region IState Methods
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
 
    }

    public virtual void Update()
    {

    }
    
    public virtual void PhysicsUpdate()
    {
        
    }
    #endregion
}
