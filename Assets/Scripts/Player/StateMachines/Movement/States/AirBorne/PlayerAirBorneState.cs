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
        if (stateMachine.PreviousState is PlayerAirBorneState) return;
        
        preservedVelocity = (stateMachine.PlayerController.transform.position - stateMachine.PlayerController.lastPosition) / Time.deltaTime;
        preservedVelocity.y = 0;
        stateMachine.PlayerController.velocity += preservedVelocity;
    }

    public override void Update()
    {
        // fall
        stateMachine.PlayerController.velocity.y -= stateMachine.PlayerController.PlayerSettings.Gravity * Time.deltaTime;
        stateMachine.PlayerController.velocity.y = Mathf.Max(stateMachine.PlayerController.velocity.y, -10);

        

        if (stateMachine.PlayerController.IsGrounded && !IsJumping)
        {
            stateMachine.PlayerController.velocity.y = 0;
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
