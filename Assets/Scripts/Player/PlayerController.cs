using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerSettings PlayerSettings;

    private CharacterController _controller;
    private Transform _cam;
    public Animator Animator { get; private set; }
    public InputController InputController;
    public Vector2 moveInput { get; private set; }
    private GroundCheck _groundCheck;
    private PlayerMovementStateMachine _movementStateMachine;
    public CharacterController CharacterController { get; private set; }
    public Vector3 velocity;
    private Vector3 _rootMotionDelta;
    public bool IsGrounded { get { return _groundCheck.IsGrounded; } }

    // buffer of preframe position, for caculate offset
    public Vector3 lastPosition { get; private set; }


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.FindWithTag("ThirdPersonCamera").transform;
        Animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        InputController = GetComponent<InputController>();
        CharacterController = GetComponent<CharacterController>();
        if (InputController == null)
        {
            Debug.LogError("InputController component not found on this GameObject. Ensure it is attached.");
        }
        _movementStateMachine = new(this);
    }

    void Start()
    {
        _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
    }

    void OnEnable()
    {
        if (InputController != null)
        {
            InputController.MoveEvent += HandleMoveInput;
        }
    }

    void OnDisable()
    {
        if (InputController != null)
        {
            InputController.MoveEvent -= HandleMoveInput;
        }
    }

    private void HandleMoveInput(Vector2 movement)
    {
        moveInput = movement;
    }

    // Update is called once per frame
    void Update()
    {

        _movementStateMachine.HandleInput();
        _movementStateMachine.Update();
        lastPosition = transform.position;

        // move character
        Vector3 moveDelta = _rootMotionDelta + velocity * Time.deltaTime;
        CharacterController.Move(moveDelta);

    }

    void FixedUpdate()
    {
        _movementStateMachine.PhysicsUpdate();
    }

    void OnAnimatorMove()
    {
        _rootMotionDelta = Animator.deltaPosition;
    }

    public void RotateCharacter(float rotationSpeed)
    {
        Vector3 moveDirection = GetInputDirection();
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetInputDirection()
    {
        Vector3 camForward = _cam.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = _cam.right;
        camRight.y = 0f;
        camRight.Normalize();
        return camForward * moveInput.y + camRight * moveInput.x;
    }
}
