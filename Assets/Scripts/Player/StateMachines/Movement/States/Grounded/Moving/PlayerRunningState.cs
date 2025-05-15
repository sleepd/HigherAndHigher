using UnityEngine;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void HandleInput()
    {

        base.HandleInput();
        if (stateMachine.PlayerController.moveInput.sqrMagnitude < stateMachine.PlayerController.PlayerSettings.WalkingToRunningThreshold)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        stateMachine.PlayerController.RotateCharacter(stateMachine.PlayerController.PlayerSettings.RotationSpeed);
    }
}
