using System;
using UnityEngine;

public class PlayerFallState : PlayerAirBorneState
{
    public PlayerFallState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.Animator.SetBool("Falling", true);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.Animator.SetBool("Falling", false);
    }

    // public override void Update()
    // {
    //     base.Update();
    //     if (stateMachine.PlayerController.velocity.y < 0) IsJumping = false;
    // }
}
