using System;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
public class InputReader : ScriptableObject,InputSystem_Actions.IPlayerActions
{

    [SerializeField] private LayerMask whatIsGround;

    public event Action OnAttackPressed;
    public event Action OnCrouchPressed;
    public event Action OnInteractPressed;
    public event Action<bool> OnSprintPressed;

    public Vector2 MovementKey { get; private set; }
    private InputSystem_Actions _controls;
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new InputSystem_Actions();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementKey = context.ReadValue<Vector2>();
        MovementKey = movementKey;
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressed?.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCrouchPressed?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInteractPressed?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnSprintPressed?.Invoke(true);
        if (context.canceled)
            OnSprintPressed?.Invoke(false);
    }
}
