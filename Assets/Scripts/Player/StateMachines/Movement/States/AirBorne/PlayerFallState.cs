using System;
using UnityEngine;

public class PlayerFallState : PlayerAirBorneState
{
    public PlayerFallState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.PlayerController.velocity.y < 0) IsJumping = false;
    }
}
