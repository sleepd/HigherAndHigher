using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerIdlingState IdlingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerController PlayerController { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }

    public PlayerMovementStateMachine(PlayerController playerController)
    {
        IdlingState = new(this);
        WalkingState = new(this);
        RunningState = new(this);
        JumpState = new(this);
        FallState = new(this);
        PlayerController = playerController;
    }

}
