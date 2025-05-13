using System;
using UnityEngine;

public class PlayerJumpState : PlayerAirBorneState
{
    public PlayerJumpState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.PlayerController.velocity.y < 0)
        {
            IsJumping = false;
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    private void Jump()
    {
        stateMachine.PlayerController.velocity.y = stateMachine.PlayerController.PlayerSettings.JumpForce;
        stateMachine.PlayerController.Animator.SetTrigger("Jump");
        IsJumping = true;
    }
}
