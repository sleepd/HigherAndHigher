using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _input;
    
    // Input Events
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action JumpEvent;
    public event Action JumpCanceledEvent;
    public event Action AttackEvent;
    public event Action RollEvent;
    public event Action InteractEvent;
    public event Action AimEvent;
    public event Action AimCanceledEvent;

    void Awake()
    {
        _input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
        _input.Player.Jump.performed += OnJumpPerformed;
        _input.Player.Jump.canceled += OnJumpCanceled;
        _input.Player.Attack.performed += OnAttackPerformed;
        _input.Player.Roll.performed += OnRollPerformed;
        _input.Player.Interact.performed += OnInteractPerformed;
        _input.Player.Aim.performed += OnAimPerformed;
        _input.Player.Aim.canceled += OnAimCanceled;

    }

    private void OnAimCanceled(InputAction.CallbackContext context)
    {
        AimCanceledEvent?.Invoke();
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpCanceledEvent?.Invoke();
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
    }
    private void OnRollPerformed(InputAction.CallbackContext context)
    {
        RollEvent?.Invoke();
    }
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    }

    void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        _input.Player.Jump.performed -= OnJumpPerformed;
        _input.Player.Jump.canceled -= OnJumpCanceled;
        _input.Player.Attack.performed -= OnAttackPerformed;
        _input.Player.Roll.performed -= OnRollPerformed;
        _input.Player.Interact.performed -= OnInteractPerformed;
        _input.Player.Aim.performed -= OnAimPerformed;
        _input.Player.Aim.canceled -= OnAimCanceled;
        _input.Player.Disable();
    }

}
