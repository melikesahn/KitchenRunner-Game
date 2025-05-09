using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private Transform _orientationTrnasform;
  [SerializeField] private float _movementSpeed;

  private Rigidbody _playerRigidbody;

  private float _horizonntalInput, _verticalInput;
  private Vector3 _movementDirection;

  private void Awake() {
    _playerRigidbody = GetComponent<Rigidbody>();
    _playerRigidbody.freezeRotation=true;
  }
  private void Update() {
    SetInput();
  }
  private void FixedUpdate() {
    SetPlayerMovement();
  }

  private void SetInput(){
    _horizonntalInput = Input.GetAxis("Horizontal");
    _verticalInput = Input.GetAxis("Vertical");
  } 
  private void SetPlayerMovement(){
    _movementDirection = _orientationTrnasform.forward * _verticalInput 
    + _orientationTrnasform.right * _horizonntalInput;
    _playerRigidbody.AddForce (_movementDirection.normalized * _movementSpeed, ForceMode.Force);
  }

}
