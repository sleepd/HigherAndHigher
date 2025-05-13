using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.velocity = Vector3.zero;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (stateMachine.PlayerController.moveInput.sqrMagnitude > stateMachine.PlayerController.PlayerSettings.MovementDeadZone)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
    
}