using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnAppQuitAction;

    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.Interact_Alternate.performed += Interact_Alternate_performed;
        inputActions.Player.Exit.performed += Exit_performed;
    }

    private void Exit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAppQuitAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Alternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
#if  !UNITY_EDITOR && UNITY_ANDROID
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        Vector2 inputVector = new Vector2(direction.x, direction.z);
        return inputVector.normalized;
#else
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
#endif
    }
}
