
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Компоненты")] 
    [SerializeField] private PlayerKeyboardController keyboardController;
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float currentSpeed;
    public float CurrentSpeed
    {
        get => currentSpeed;
        set
        {
            if (value < 0f) currentSpeed = 0f;
            else if (value > 300f) currentSpeed = 300f;
            else currentSpeed = value;
        }
    }
    private float rotationSpeed;
    public float RotationSpeed
    {
        get => rotationSpeed;
        set
        {
            if (value < 100f) rotationSpeed = 100f;
            else if (value > 500f) rotationSpeed = 500f;
            else rotationSpeed = value;
        }
    }
    public bool IsCrouching { get; private set; }
    public bool IsSprinting { get; private set; }
    public Vector3 MoveDirection { get; private set; }
    private float _horizontal;
    private float _vertical;
    private float _stepSoundTimer;
    private bool _stepStarted;
    
    public bool isEnableToRotation = true;
    public bool isStaminaEnough;
    
    private Vector3 _mousePosition;
    private Vector3 _characterPosition;

    public Action OnMoveForward;
    public Action OnMoveBack;
    public Action OnMoveLeft;
    public Action OnMoveRight;
    public Action OnJump;
    public Action OnStep;
    public Action OnTrySprinting;
    public Action OnTryCrouching;
    public Action OnStartSprinting;
    public Action OnStartCrouching;
    public Action OnEndSprinting;
    public Action OnEndCrouching;
    public Action OnStandardMoving;
    public Action OnSetAttackRotation;
    public Action OnResetRotation;
    private bool _isMainNotNull;

    private void OnEnable()
    {
        keyboardController.OnMoveForward += MoveForward;
        keyboardController.OnMoveLeft += MoveLeft;
        keyboardController.OnMoveRight += MoveRight;
        keyboardController.OnMoveBack += MoveBack;
        keyboardController.OnJump += Jump;
        keyboardController.OnSprint += Sprinting;
        keyboardController.OnCrouch += Crouching;
        keyboardController.OnStandardMove += StandardMoving;
        keyboardController.OnSlowdown += Slowdown;
        keyboardController.OnRotation += Rotation;
    }

    private void OnDisable()
    {
        keyboardController.OnMoveForward -= MoveForward;
        keyboardController.OnMoveLeft -= MoveLeft;
        keyboardController.OnMoveRight -= MoveRight;
        keyboardController.OnMoveBack -= MoveBack;
        keyboardController.OnJump -= Jump;
        keyboardController.OnSprint -= Sprinting;
        keyboardController.OnCrouch -= Crouching;
        keyboardController.OnStandardMove -= StandardMoving;
        keyboardController.OnSlowdown -= Slowdown;
        keyboardController.OnRotation -= Rotation;
        
    }

    void Start()
    {
        _isMainNotNull = Camera.main != null;
        _transform = _transform == null ? GetComponent<Transform>() : _transform;
        _rigidbody = _rigidbody == null ? GetComponent<Rigidbody2D>() : _rigidbody;
        OnResetRotation?.Invoke();
        OnTrySprinting?.Invoke();
    }

    public void MoveForward()
    {
        Move();
        OnMoveForward?.Invoke();
    }

    public void MoveBack()
    {
        Move();
        OnMoveBack?.Invoke();
    }

    public void MoveLeft()
    {
        Move();
        OnMoveLeft?.Invoke();
    }

    public void MoveRight()
    {
        Move();
        OnMoveRight?.Invoke();
    }

    private void StandardMoving()
    {
        OnStandardMoving?.Invoke();
        StopToSprint();
        StopToCrouch();
    }
    
    private void Crouching()
    {
        OnTryCrouching?.Invoke();
        if (isStaminaEnough)
        {
            OnStartCrouching?.Invoke();
            StopToSprint();
            IsCrouching = true;
        }
        else StopToCrouch();
    }
    
    private void Sprinting()
    {
        OnTrySprinting?.Invoke();
        if (isStaminaEnough)
        {
            OnStartSprinting?.Invoke();
            StopToCrouch();
            IsSprinting = true;
        }
        else StopToSprint();
    }

    private void StopToSprint()
    {
        if (IsSprinting)
        {
            OnEndSprinting?.Invoke();
            IsSprinting = false;
        }
    }

    private void StopToCrouch()
    {
        if (IsCrouching)
        {
            OnEndCrouching?.Invoke();
            IsCrouching = false;
        }
    }
    
    public void Jump()
    {
        OnJump?.Invoke();
    }
    
    public void Rotation()
    {
        if (!isEnableToRotation) return;
        if (_isMainNotNull) _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _characterPosition = transform.position;
        float angle = Mathf.Atan2(_mousePosition.y - _characterPosition.y, _mousePosition.x - _characterPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), RotationSpeed * Time.deltaTime);
        
    }

    private void Move()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        MoveDirection = new Vector3(_horizontal, _vertical, 0f);
        MoveDirection.Normalize();
        _rigidbody.velocity = Vector3.ClampMagnitude(MoveDirection, 1) * (CurrentSpeed * Time.fixedDeltaTime);
        OnStep?.Invoke();
    }

    public void Slowdown()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void ResetRotationSpeed()
    {
        OnResetRotation?.Invoke();
    }
    
    public void SetAttackRotationSpeed(float value)
    {
        OnSetAttackRotation?.Invoke();
    }
}
