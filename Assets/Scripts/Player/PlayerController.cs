using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public enum CameraTag
    {
        ThirdPerson,
        Aiming
    }

public class PlayerController : MonoBehaviour
{
    public PlayerSettings PlayerSettings;

    private CharacterController _controller;
    public List<CinemachineCamera> cameras;

    // public CinemachineCamera ThirdPersonCam;
    // public CinemachineInputAxisController ThirdPersonCamInputController;
    // public CinemachineCamera AimingCam;
    public CinemachineCamera CurrentCam;
    public Transform AimTarget;

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
    public float MoveSpeedFactor = 1f;
    LayerMask targetLayers;
    [SerializeField] Transform firePosition;
    [SerializeField] float aimOffsetY = 0f;

    private float _fireCooldowe = 0;

    private float moveFactor = 1f;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        cameras = new();
        cameras.Add(GameObject.FindWithTag("ThirdPersonCamera").GetComponent<CinemachineCamera>());
        cameras.Add(GameObject.FindWithTag("AimCamera").GetComponent<CinemachineCamera>());
        CurrentCam = cameras[0];
        Animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        InputController = GetComponent<InputController>();
        CharacterController = GetComponent<CharacterController>();
        if (InputController == null)
        {
            Debug.LogError("InputController component not found on this GameObject. Ensure it is attached.");
        }
        _movementStateMachine = new(this);
        targetLayers = LayerMask.GetMask("Ground", "Enemy");
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
        moveDelta *= moveFactor;
        if (moveDelta != Vector3.zero) CharacterController.Move(moveDelta);

        // handle fire cooldown
        if (_fireCooldowe > 0)
        {
            _fireCooldowe = MathF.Max(0, _fireCooldowe - Time.deltaTime);
        }
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
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void AlignCamera()
    {
        Vector3 cameraDir = CurrentCam.transform.forward;
        cameraDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(cameraDir);
        transform.rotation = targetRotation;

    }

    public Vector3 GetInputDirection()
    {
        Vector3 camForward = CurrentCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = CurrentCam.transform.right;
        camRight.y = 0f;
        camRight.Normalize();
        return camForward * moveInput.y + camRight * moveInput.x;
    }

    public void Shoot(int number)
    {
        if (_fireCooldowe == 0)
        {
            for (int i = 0; i < number; i++)
            {

                GameObject bullet = Instantiate(PlayerSettings.BulletPrefab, firePosition.position, CurrentCam.transform.rotation);
                Ray ray = new Ray(CurrentCam.transform.position, CurrentCam.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayers))
                {
                    Vector3 hitPoint = hit.point;
                    bullet.GetComponent<Bullet>().SetTargetPosition(hitPoint);
                }
                else
                {
                    bullet.GetComponent<Bullet>().SetTargetDir(CurrentCam.transform.forward);
                }
            }
            _fireCooldowe = PlayerSettings.fireRate;
        }
    }

    public void SwitchCamera(int index)
    {
        foreach (CinemachineCamera cam in cameras)
        {
            cam.Priority = 0;
        }
        cameras[index].Priority = 100;
        CurrentCam = cameras[index];
    }

    public void TakeDamege()
    {
        Debug.Log("22222222222");
        // die!
        Die();
    }

    private void Die()
    {
        moveFactor = 0;
        GameManager.Instance.CurrentLevelManager.ResetPlayer();
    }

    public void ResetState()
    {
        moveFactor = 1;
    }
}
