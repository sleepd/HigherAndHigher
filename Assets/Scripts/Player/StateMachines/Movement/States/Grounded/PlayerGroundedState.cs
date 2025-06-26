using System;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputController.JumpEvent += HandleJumpInput;
        stateMachine.PlayerController.InputController.AimEvent += HandleAim;
        stateMachine.PlayerController.InputController.InteractEvent += HandleInteract;
    }



    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.JumpEvent -= HandleJumpInput;
        stateMachine.PlayerController.InputController.AimEvent -= HandleAim;
        stateMachine.PlayerController.InputController.InteractEvent -= HandleInteract;
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.PlayerController.IsGrounded)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
        stateMachine.PlayerController.velocity.y = -1f;
    }

    public override void HandleInput()
    {
        float velocity = stateMachine.PlayerController.moveInput.sqrMagnitude * stateMachine.PlayerController.MoveSpeedFactor;
        stateMachine.PlayerController.Animator.SetFloat("Velocity", velocity);
    }

    public virtual void HandleJumpInput()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected void HandleAim()
    {
        stateMachine.ChangeState(stateMachine.AimingState);
    }

    protected void HandleInteract()
    {
        Debug.Log("Try to interact!");
        Vector3 origin = stateMachine.PlayerController.transform.position + Vector3.up * 1.0f;
        Vector3 direction =stateMachine.PlayerController.transform.forward;
        Vector3 checkPosition = origin + direction * 1f;
        float radius = 10f;
        int layerMask = 1 << 11;

        Collider[] hits = Physics.OverlapSphere(checkPosition, radius, layerMask, QueryTriggerInteraction.Collide);


        foreach (Collider hit in hits)
        {
            Debug.Log("hit!");
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

}
