using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public event Action OnPlayerJumped;
  [SerializeField] private Transform _orientationTransform;
  [SerializeField] private float _movementSpeed;
  [SerializeField] private KeyCode _movementKey;
  [SerializeField] private KeyCode _jumpKey;
  [SerializeField] private float _jumpForce;
  [SerializeField] private float _jumpCooldown;
  [SerializeField] private float _airMultiplier;
  [SerializeField] private float _airDrag;
  [SerializeField] private bool _canJump;
  [SerializeField] private KeyCode _slideKey;
  [SerializeField] private float _slideMultiplier;
  [SerializeField] private float _slideDrag;
  [SerializeField] private float _playerHeight;
  [SerializeField] private LayerMask _groundLayer;
  [SerializeField] private float _groundDrag;



  private Rigidbody _playerRigidbody;

  private float _horizonntalInput, _verticalInput;
  private Vector3 _movementDirection;
  private bool _isSliding;
  private StateController _stateController;

  private void Awake()
  {
    _stateController = GetComponent<StateController>();
    _playerRigidbody = GetComponent<Rigidbody>();
    _playerRigidbody.freezeRotation = true;
  }
  private void Update()
  {
    SetInput();
    SetStates();
    SetPlayerDrag();
    LimitPlayerSpeed();
  }
  private void FixedUpdate()
  {
    SetPlayerMovement();
  }

  private void SetInput()
  {
    _horizonntalInput = Input.GetAxis("Horizontal");
    _verticalInput = Input.GetAxis("Vertical");

    if (Input.GetKeyDown(_slideKey))
    {
      _isSliding = true;
    }
    else if (Input.GetKeyDown(_movementKey))
    {
      _isSliding = false;
    }

    else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
    {
      //zıplama
      _canJump = false;
      SetPlayerJumping();
      Invoke(nameof(ResetJumping), _jumpCooldown);
    }
  }

  private void SetStates()
  {
    var movementDirection = GetMovementDirection();
    var isGrounded = IsGrounded();
    var isSliding = IsSliding();
    var currentState = _stateController.GetCurrentState();

    var newState = currentState switch
    {
      _ when movementDirection == Vector3.zero && isGrounded && !IsSliding() => PlayerState.Idle,
      _ when movementDirection != Vector3.zero && isGrounded && !IsSliding() => PlayerState.Move,
      _ when movementDirection != Vector3.zero && isGrounded && IsSliding() => PlayerState.Slide,
      _ when movementDirection == Vector3.zero && isGrounded && IsSliding() => PlayerState.SlideIdle,
      _ when !_canJump && !isGrounded => PlayerState.Jump,
      _ => currentState
    };

    if (newState != currentState)
    {
      _stateController.ChangeState(newState);

    }
    
  }

  private void SetPlayerMovement()
  {
    _movementDirection = _orientationTransform.forward * _verticalInput
    + _orientationTransform.right * _horizonntalInput;

    float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Jump => _airMultiplier,
            PlayerState.Slide => _slideMultiplier,
            _ => 1f
        };

     _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);

  }

  private void SetPlayerDrag()
  {
    _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };
  }

  private void LimitPlayerSpeed()
  {
    Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0, _playerRigidbody.linearVelocity.z);
    if (flatVelocity.magnitude > _movementSpeed)
    {
      Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
      _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
    }
  }

  private void SetPlayerJumping()
  {
    OnPlayerJumped?.Invoke();
    _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
    _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
  }
  private void ResetJumping()
  {
    _canJump = true;
  }
  private bool IsGrounded()
  {
    return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
  }

  private Vector3 GetMovementDirection()
  {
    return _movementDirection.normalized;
  }
  
   public bool IsSliding()
  {
    return _isSliding;
  }

}
