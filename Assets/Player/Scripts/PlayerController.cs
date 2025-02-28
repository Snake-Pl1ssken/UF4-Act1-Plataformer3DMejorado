using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : Entity
{
    public static PlayerController instance;

    [Header("Movement Sttings")]
    [SerializeField] float speedWalk = 5f;
    [SerializeField] float speedRun = 20f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 10f;
    [Space]
    [Space]
    [Header("InputActions")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;

    public enum OrientationMode
    {
        ToMoveDirection,
        ToCameraForward,
        ToTarget,
    };
    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.ToMoveDirection;
    [SerializeField] Transform orientationTarget;
    [SerializeField] float angularSpeed = 720f;

    CharacterController characterController;

    Camera mainCamera;

    HurtCollider hurtCollider;
    Ragdollizer ragdollizer;

    HitCollider hitCollider;


    float speed;

    protected override void Awake()
    {
        base.Awake();
        
        instance = this;

        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        hitCollider = GetComponentInChildren<HitCollider>();
        hurtCollider = GetComponent<HurtCollider>();
        ragdollizer = GetComponentInChildren<Ragdollizer>();

        speed = speedWalk;
    }
    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();

        move.action.performed += OnMove;
        move.action.started += OnMove;
        move.action.canceled += OnMove;

        jump.action.performed += OnJump;

        run.action.started += OnRun;
        run.action.canceled += OnRun;

        hurtCollider.onHitRecived.AddListener(OnHitRecived);
        hitCollider.onHitDelivered.AddListener(OnHitDelivered);
    }


    private void Update()
    {
        UpdateMovementOnPlane();
        UpdateVerticalMovement();
        UpdateOrientation();
        UpdateAnimation();
    }
    Vector3 lastNormalizedVelocity = Vector3.zero;
    private void UpdateMovementOnPlane()
    {
        Vector3 movement = mainCamera.transform.right * rawMove.x + mainCamera.transform.forward * rawMove.z;

        float oldMovementMAgnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane = Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMAgnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);
        lastNormalizedVelocity = movementProjectedOnPlane;
    }
    float gravity = -9.8f;
    float verticalVelocity;

    void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        lastNormalizedVelocity.y = verticalVelocity;
        if (characterController.isGrounded)
        {
            verticalVelocity = verticalSpeedOnGrounded;
        }
        if (mustJump)
        {
            mustJump = false;
            //Debug.Log("Jump");
            if (characterController.isGrounded)
            {
                verticalVelocity = jumpVelocity;

            }
        }
    }

    void UpdateOrientation()
    {
        Vector3 desiredDirection = Vector3.forward;
        switch (orientationMode)
        {
            case OrientationMode.ToMoveDirection:
                desiredDirection = lastNormalizedVelocity;
                break;
            case OrientationMode.ToCameraForward:
                desiredDirection = mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection = orientationTarget.position - transform.position;
                break;
        }
        desiredDirection.y = 0f;

        float angleToApply = angularSpeed * Time.deltaTime;
        // Distancia angular entre transform.forward y desiredDirection
        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
        float realAngleToApply = Mathf.Sign(angularDistance) * Mathf.Min(angleToApply, Mathf.Abs(angularDistance));
        Quaternion rotationToApply = Quaternion.AngleAxis(realAngleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }

    private void OnHitDelivered(HitCollider agressor, HurtCollider victim)
    {
        if (victim.CompareTag("Enemy"))
        {
            Debug.Log("jumpVelocity original: " + jumpVelocity);
            jumpVelocity += 5;
            Debug.Log("jumpVelocity new: " + jumpVelocity);
        }
        //arg0 reparte arg1 recibe
    }
    private void OnHitRecived(HitCollider hitCollider, HurtCollider hurtCollider)
    {
        Debug.Log("Hit");
        ragdollizer.RagDollizer();
        Invoke(nameof(Deactivate), 2f);
        Invoke(nameof(Resurrect), 3f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        ragdollizer.DeRagdollizer();
    }

    void Resurrect()
    {
        gameObject.SetActive(true);
    }

    Vector3 rawMove = Vector3.zero;

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0f, rawInput.y);
    }
    bool mustJump;
    private void OnJump(InputAction.CallbackContext context)
    {
        mustJump = true;
    }
    void OnRun(InputAction.CallbackContext context)
    {
        speed = run.action.IsPressed() ? speedRun : speedWalk;
    }
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();

        move.action.performed -= OnMove;
        move.action.started -= OnMove;
        move.action.canceled -= OnMove;

        run.action.started -= OnRun;
        run.action.canceled -= OnRun;

        jump.action.performed -= OnJump;

        hurtCollider.onHitRecived.RemoveListener(OnHitRecived);
        hitCollider.onHitDelivered.RemoveListener(OnHitDelivered);
    }

    protected override float GetCurrentVerticalSpeed()
    {
        return verticalVelocity;
    }
    protected override float GetJumpSpeed()
    {
        return jumpVelocity;
    }

    protected override bool IsRunning()
    { 
        return speed == speedRun;
    }
    protected override bool IsGrounded()
    {
        return characterController.isGrounded;
    }
    protected override Vector3 GetLastNormalizedVelocity()
    {
        return lastNormalizedVelocity;
    }
}
