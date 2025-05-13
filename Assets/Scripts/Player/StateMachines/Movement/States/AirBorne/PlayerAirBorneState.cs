using UnityEngine;

public class PlayerAirBorneState : PlayerMovementState
{
    public bool IsJumping;
    public PlayerAirBorneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Update()
    {
        stateMachine.PlayerController.velocity.y -= stateMachine.PlayerController.PlayerSettings.Gravity * Time.deltaTime;
        stateMachine.PlayerController.velocity.y = Mathf.Max(stateMachine.PlayerController.velocity.y, -10);
        if (stateMachine.PlayerController.IsGrounded && !IsJumping)
        {
            stateMachine.PlayerController.velocity.y = 0;
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
