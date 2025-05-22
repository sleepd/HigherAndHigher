using UnityEngine;

public class PlayerAimingState : PlayerGroundedState
{
    public PlayerAimingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.AlignCamera();
        stateMachine.PlayerController.InputController.AimCanceledEvent += EndAiming;
        stateMachine.PlayerController.InputController.JumpEvent -= HandleJumpInput;
        stateMachine.PlayerController.InputController.AttackEvent += HanldeRangedAttack;
        stateMachine.PlayerController.AimingCam.Priority = 100;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.AimCanceledEvent -= EndAiming;
        stateMachine.PlayerController.InputController.AttackEvent -= HanldeRangedAttack;
        stateMachine.PlayerController.AimingCam.Priority = 10;
    }

    public override void HandleInput()
    {

    }

    private void EndAiming()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    private void HanldeRangedAttack()
    {
        {
            for (int i = 0; i < 3; i++)
            {
                stateMachine.PlayerController.Shoot();
            }
        }
    }
}
