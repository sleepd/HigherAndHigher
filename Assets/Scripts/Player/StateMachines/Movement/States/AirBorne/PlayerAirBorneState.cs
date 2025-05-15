using UnityEngine;

public class PlayerAirBorneState : PlayerMovementState
{
    public bool IsJumping;
    public Vector3 preservedVelocity;
    public bool IsJumpPressed;
    public float TimeSinceJumpPressed;
    public PlayerAirBorneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        // Air jump / jump buffering
        stateMachine.PlayerController.InputController.JumpEvent += HandleJumpInput;
        IsJumpPressed = false;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.JumpEvent -= HandleJumpInput;
    }

    public override void Update()
    {
        // add gravity
        stateMachine.PlayerController.velocity.y -= stateMachine.PlayerController.PlayerSettings.Gravity * Time.deltaTime;
        stateMachine.PlayerController.velocity.y = Mathf.Max(stateMachine.PlayerController.velocity.y, -10);

        if (IsJumpPressed)
        {
            TimeSinceJumpPressed += Time.deltaTime;
        }

        // landing
        if (stateMachine.PlayerController.IsGrounded && !IsJumping)
        {
            // check jump buffer
            if (IsJumpPressed && TimeSinceJumpPressed < stateMachine.PlayerController.PlayerSettings.JumpBufferingThreshold)
            {
                IsJumpPressed = false;
                stateMachine.PlayerController.RotateCharacter(stateMachine.PlayerController.PlayerSettings.RotationSpeed * 100);
                stateMachine.ChangeState(stateMachine.JumpState);
                return;
            }
            stateMachine.PlayerController.velocity.y = 0;
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

    protected void HandleJumpInput()
    {
        // for jump buffering and double jump
        IsJumpPressed = true;
        TimeSinceJumpPressed = 0;
    }
}
