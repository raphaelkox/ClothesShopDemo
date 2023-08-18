using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public event EventHandler<Vector2InputEventArgs> OnMovementInput;
    public class Vector2InputEventArgs : EventArgs {
        public Vector2 inputValue;
    }

    public event EventHandler OnInteractPerformed;
    public event EventHandler OnInteractCanceled;

    public event EventHandler OnSwitchInteractionPerformed;
    public event EventHandler OnSwitchInteractionCanceled;

    private GameControls gameControls;

    private void Start() {
        gameControls = new GameControls();

        gameControls.Player.Movement.performed += Movement_updated;
        gameControls.Player.Movement.canceled += Movement_updated;

        gameControls.Player.Interact.performed += Interact_performed;
        gameControls.Player.Interact.canceled += Interact_canceled;

        gameControls.Player.SwithInteraction.performed += SwithInteraction_performed;
        gameControls.Player.SwithInteraction.canceled += SwithInteraction_canceled;

        gameControls.Player.Enable();
        //TODO disable Menu Controls
    }

    private void Movement_updated(InputAction.CallbackContext ctx) {
        OnMovementInput?.Invoke(this, new Vector2InputEventArgs{ 
            inputValue = ctx.ReadValue<Vector2>()
        });
    }

    private void Interact_performed(InputAction.CallbackContext obj) {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_canceled(InputAction.CallbackContext obj) {
        OnInteractCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void SwithInteraction_performed(InputAction.CallbackContext obj) {
        OnSwitchInteractionPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void SwithInteraction_canceled(InputAction.CallbackContext obj) {
        OnSwitchInteractionCanceled?.Invoke(this, EventArgs.Empty);
    }
}
