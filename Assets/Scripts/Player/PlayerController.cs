using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerSettings PlayerSettings;

    private CharacterController _controller;
    private Transform _cam;
    public Animator Animator {get; private set;}
    public InputController InputController;
    public Vector2 moveInput {get; private set;}
    private GroundCheck _groundCheck;
    private PlayerMovementStateMachine _movementStateMachine;
    public CharacterController CharacterController { get; private set;}
    public Vector3 velocity;
    public bool IsGrounded {get {return _groundCheck.IsGrounded;}}


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
        if(InputController != null)
        {
            InputController.MoveEvent += HandleMoveInput;
        }
    }

    void OnDisable()
    {
        if(InputController != null)
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
        // if (_groundCheck.IsGrounded)
        // {
        //     if (_moveInput.sqrMagnitude > 0.01f)
        //     // rotate character
        //     {        
        //         Vector3 camForward = _cam.forward;
        //         camForward.y = 0;
        //         camForward.Normalize();
        //         Vector3 camRight = _cam.right;
        //         camRight.y = 0f;
        //         camRight.Normalize();
        //         Vector3 moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;
        //         Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerSettings.rotationSpeed * Time.deltaTime);
        //     }
        //     _animator.SetFloat("Velocity", _moveInput.magnitude);
        // }
        // CharacterController.Move(velocity * Time.deltaTime);
        
    }

    void FixedUpdate()
    {
        _movementStateMachine.PhysicsUpdate();
    }

    void OnAnimatorMove()
    {
        Vector3 rootMotionDelta = Animator.deltaPosition;
        rootMotionDelta += velocity * Time.deltaTime;
        CharacterController.Move(rootMotionDelta);
    }

    public void RotateCharacter()
    {
        Vector3 camForward = _cam.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = _cam.right;
        camRight.y = 0f;
        camRight.Normalize();
        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, PlayerSettings.RotationSpeed * Time.deltaTime);
    }
}
