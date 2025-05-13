using UnityEngine;

public class PlayerAirBorneState : PlayerMovementState
{
    public bool IsJumping;
    public Vector3 preservedVelocity;
    public PlayerAirBorneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        preservedVelocity = stateMachine.PlayerController.Animator.deltaPosition;
        preservedVelocity.y = 0;
    }

    public override void Update()
    {
        // fall
        stateMachine.PlayerController.velocity.y -= stateMachine.PlayerController.PlayerSettings.Gravity * Time.deltaTime;
        stateMachine.PlayerController.velocity.y = Mathf.Max(stateMachine.PlayerController.velocity.y, -10);

        // keep forward
        stateMachine.PlayerController.velocity += preservedVelocity;

        if (stateMachine.PlayerController.IsGrounded && !IsJumping)
        {
            stateMachine.PlayerController.velocity.y = 0;
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
