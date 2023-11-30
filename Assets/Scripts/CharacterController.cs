using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower;

    private CharacterController _characterController;
    private Vector3 _walkDirection;
    private Vector3 _velocity;
    private float _speed;
    private bool isNoteVisible = false; // Устанавливается в true, когда записка открыта

    private void Start()
    {
        _speed = _speedWalk;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isNoteVisible)
            HandleMovementInput();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            Walk(_walkDirection);
            DoGravity(_characterController.isGrounded);
        }
    }

    private void HandleMovementInput()
    {
        Jump(Input.GetKey(KeyCode.Space) && _characterController.isGrounded);
        Sit(Input.GetKey(KeyCode.LeftControl));

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _walkDirection = transform.right * x + transform.forward * z;
    }

    private void Walk(Vector3 direction)
    {
        if (IsCharacterControllerValid())
        {
            _characterController.Move(direction * _speedWalk * Time.fixedDeltaTime);
        }
    }

    private void DoGravity(bool isGrounded)
    {
        if (IsCharacterControllerValid())
        {
            if (isGrounded && _velocity.y < 0)
                _velocity.y = -1f;
            _velocity.y -= _gravity * Time.fixedDeltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
    }

    private void Jump(bool canJump)
    {
        if (canJump)
            _velocity.y = _jumpPower;
    }

    private void Sit(bool canSit)
    {
        _characterController.height = canSit ? 1f : 2f;
    }

    private bool IsCharacterControllerValid()
    {
        return _characterController != null && _characterController.enabled && _characterController.gameObject.activeSelf;
    }
}
