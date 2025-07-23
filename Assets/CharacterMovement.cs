using Member.Isac._1.Scripts.Entities;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float gravity = -9.81f;
    public CharacterController characterController;
    [SerializeField] private Transform cameraRoot;

    public bool CanManualMovement { get; set; } = true;
    public bool CanSprint { get; set; } = true;

    [field: SerializeField] public float MoveSpeed { get; set; } = 3f;
    private Vector3 _movementInput;
    private Vector3 _velocity;
    private float _verticalVelocity;
    private float _yaw;
    private float _pitch;

    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void SetMoveSpeed(float value)
            => MoveSpeed = value;

    public void SetMovementDirection(Vector2 input)
    {
        _movementInput = new Vector3(input.x, 0, input.y).normalized;
    }

    public void AddLookInput(Vector2 lookDelta, float sensitivity = 1f)
    {
        _yaw += lookDelta.x * sensitivity;
        _pitch -= lookDelta.y * sensitivity;
        _pitch = Mathf.Clamp(_pitch, -85f, 85f);
    }

    public void SetColliderHeight(float value) => DOTween.To(() => characterController.height, (x) => characterController.height = x,value,0.1f);

    private void Update()
    {
        if (Time.timeScale == 0) return;
        Rotate();
        CalculateMovement();
        ApplyGravity();
        Move();

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        AddLookInput(new Vector2(mouseX, mouseY));
    }

    public void Sprint(bool isRun) => MoveSpeed = isRun ? 6f : 3f;

    private void Rotate()
    {
        transform.parent.rotation = Quaternion.Euler(_pitch, _yaw, 0);
        if (cameraRoot) cameraRoot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    private void CalculateMovement()
    {
        if (CanManualMovement)
        {
            Vector3 direction = transform.TransformDirection(_movementInput);
            _velocity = direction * MoveSpeed;
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && _verticalVelocity < 0)
            _verticalVelocity = -0.03f;
        else
            _verticalVelocity += gravity * Time.deltaTime;

        _velocity.y = _verticalVelocity;
    }

    private void Move()
    {
        characterController.Move(_velocity * Time.deltaTime);
    }

    public void StopImmediately()
    {
        _movementInput = Vector3.zero;
    }
}