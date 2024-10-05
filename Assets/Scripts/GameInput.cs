using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public event EventHandler OnInteractAcion;
    public event EventHandler OnInteractAlternateAcion;
    public void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAcion?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAcion?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 InputVector = inputActions.Player.Move.ReadValue<Vector2>();

        InputVector = InputVector.normalized;

        return InputVector;
    }
}
