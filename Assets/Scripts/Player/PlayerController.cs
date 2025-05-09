using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerSettings playerSettings;

    private CharacterController _controller;
    private Transform _cam;
    private Animator _animator;
    private InputController _inputController;
    private Vector2 _moveInput;
    private GroundCheck _groundCheck;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = GameObject.FindWithTag("ThirdPersonCamera").transform;
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        _inputController = GetComponent<InputController>();
        if (_inputController == null)
        {
            Debug.LogError("InputController component not found on this GameObject. Ensure it is attached.");
        }
    }

    void OnEnable()
    {
        if(_inputController != null)
        {
            _inputController.MoveEvent += HandleMoveInput;
        }
    }

    void OnDisable()
    {
        if(_inputController != null)
        {
            _inputController.MoveEvent -= HandleMoveInput;
        }
    }

    private void HandleMoveInput(Vector2 movement)
    {
        _moveInput = movement;
    }

    // Update is called once per frame
    void Update()
    {
        if (_groundCheck.IsGrounded)
        {
            if (_moveInput.sqrMagnitude > 0.01f)
            // rotate character
            {        
                Vector3 camForward = _cam.forward;
                camForward.y = 0;
                camForward.Normalize();
                Vector3 camRight = _cam.right;
                camRight.y = 0f;
                camRight.Normalize();
                Vector3 moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerSettings.rotationSpeed * Time.deltaTime);
            }


            _animator.SetFloat("Velocity", _moveInput.magnitude);
        }
        
        

    }
}
