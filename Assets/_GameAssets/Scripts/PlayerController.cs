using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private Transform _orientationTransform;
  [SerializeField] private float _movementSpeed;
  [SerializeField] private KeyCode _movementKey;
  [SerializeField] private KeyCode _jumpKey;
  [SerializeField] private float _jumpForce;
  [SerializeField] private float _jumpCooldown;
  [SerializeField] private bool _canJump ;
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

  private void Awake()
  {
    _playerRigidbody = GetComponent<Rigidbody>();
    _playerRigidbody.freezeRotation = true;
  }
  private void Update()
  {
    SetInput();
    SetPlayerDrag();
    LimitPlayerSpeed();
  }
  private void FixedUpdate() {
    SetPlayerMovement();
  }

  private void SetInput(){
    _horizonntalInput = Input.GetAxis("Horizontal");
    _verticalInput = Input.GetAxis("Vertical");

    if (Input.GetKeyDown(_slideKey))
    {
      _isSliding = true;
      Debug.Log("kayıyor");
    }
    else if (Input.GetKeyDown(_movementKey))
    {
      _isSliding = false;
      Debug.Log("hareket ediyor");
    }

    else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
    {
      //zıplama
      _canJump = false;
      SetPlayerJumping();
      Invoke(nameof(ResetJumping), _jumpCooldown);
    }
  }

  private void SetPlayerMovement()
  {
    _movementDirection = _orientationTransform.forward * _verticalInput
    + _orientationTransform.right * _horizonntalInput;
    if (_isSliding)
    {
      _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * _slideMultiplier, ForceMode.Force);
    }
    else
    {
       _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }
   
  }

  private void SetPlayerDrag()
  {
    if (_isSliding)
    {
      _playerRigidbody.linearDamping = _slideDrag;
    }
    else
    {
      _playerRigidbody.linearDamping = _groundDrag;
    }
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
    _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
    _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
  }
  private void ResetJumping(){
    _canJump=true;
  }
  private bool IsGrounded(){
    return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
  }

}
