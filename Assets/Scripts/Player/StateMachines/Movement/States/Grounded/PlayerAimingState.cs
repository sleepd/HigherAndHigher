using Unity.Cinemachine;
using UnityEngine;

public class PlayerAimingState : PlayerGroundedState
{
    private Quaternion _lookDirection;
    public PlayerAimingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.AlignCamera();
        stateMachine.PlayerController.InputController.AimCanceledEvent += EndAiming;
        stateMachine.PlayerController.InputController.JumpEvent -= HandleJumpInput;
        stateMachine.PlayerController.InputController.AttackEvent += HandleRangedAttack;
        stateMachine.PlayerController.InputController.LookEvent += HandleLook;
        stateMachine.PlayerController.MoveSpeedFactor = 0.6f;
        _lookDirection = stateMachine.PlayerController.CurrentCam.transform.rotation;
        stateMachine.PlayerController.CurrentCam.GetComponent<CinemachineInputAxisController>().enabled = false;
        stateMachine.PlayerController.SwitchCamera((int)CameraTag.Aiming);

        stateMachine.PlayerController.Animator.SetBool("Aiming", true);

    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.AimCanceledEvent -= EndAiming;
        stateMachine.PlayerController.InputController.AttackEvent -= HandleRangedAttack;
        stateMachine.PlayerController.SwitchCamera((int)CameraTag.ThirdPerson);
        stateMachine.PlayerController.CurrentCam.GetComponent<CinemachineInputAxisController>().enabled = true;
        stateMachine.PlayerController.InputController.LookEvent -= HandleLook;
        stateMachine.PlayerController.MoveSpeedFactor = 1f;
        stateMachine.PlayerController.Animator.SetBool("Aiming", false);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        stateMachine.PlayerController.RotateCharacter(stateMachine.PlayerController.PlayerSettings.RotationSpeed);
    }

    public override void Update()
    {
        base.Update();
        stateMachine.PlayerController.AimTarget.rotation = _lookDirection;
        // maybe I should cache this, sync the 3rd person camera
        stateMachine.PlayerController.cameras[0].GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = stateMachine.PlayerController.CurrentCam.transform.rotation.eulerAngles.y;
    }



    private void EndAiming()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    private void HandleRangedAttack()
    {
        stateMachine.PlayerController.Shoot(3);
    }

    private void HandleLook(Vector2 lookInput)
    {
        float sensitivity = 0.1f;
        Vector3 currentEuler = stateMachine.PlayerController.AimTarget.rotation.eulerAngles;

        float yaw = currentEuler.y + lookInput.x * sensitivity;
        float pitch = NormalizeAngle(currentEuler.x - lookInput.y * sensitivity);
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        _lookDirection = targetRotation;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
