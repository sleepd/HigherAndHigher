using UnityEngine;

public class PlayerWalkingState : PlayerGroundedState
{
    public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (stateMachine.PlayerController.moveInput.sqrMagnitude < stateMachine.PlayerController.PlayerSettings.MovementDeadZone)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        else if (stateMachine.PlayerController.moveInput.sqrMagnitude > stateMachine.PlayerController.PlayerSettings.WalkingToRunningThreshold)
        {
            stateMachine.ChangeState(stateMachine.RunningState);
        }
        stateMachine.PlayerController.RotateCharacter();
    }
}
