using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    //Player Map
    public event EventHandler<Vector2InputEventArgs> OnPlayer_MovementInput;
    public class Vector2InputEventArgs : EventArgs {
        public Vector2 inputValue;
    }

    public event EventHandler OnPlayer_InteractPerformed;
    public event EventHandler OnPlayer_InteractCanceled;
    //Menu Map
    public event EventHandler OnMenu_UpPerformed;
    public event EventHandler OnMenu_DownPerformed;
    public event EventHandler OnMenu_LeftPerformed;
    public event EventHandler OnMenu_RightPerformed;
    public event EventHandler OnMenu_AcceptPerformed;
    public event EventHandler OnMenu_CancelPerformed;

    private GameControls gameControls;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        gameControls = new GameControls();

        //Player Map
        gameControls.Player.Movement.performed += Movement_updated;
        gameControls.Player.Movement.canceled += Movement_updated;

        gameControls.Player.Interact.performed += Interact_performed;
        gameControls.Player.Interact.canceled += Interact_canceled;

        gameControls.Player.Enable();

        gameControls.Menu.Up.performed += Up_performed;
        gameControls.Menu.Down.performed += Down_performed;
        gameControls.Menu.Left.performed += Left_performed;
        gameControls.Menu.Right.performed += Right_performed;
        gameControls.Menu.Accept.performed += Accept_performed;
        gameControls.Menu.Cancel.performed += Cancel_performed;

        gameControls.Menu.Disable();
    }
    
    //Player Map
    public void EnablePlayerInput() {
        gameControls.Player.Enable();
    }

    public void DisablePlayerInput() {
        gameControls.Player.Disable();
    }

    private void Movement_updated(InputAction.CallbackContext ctx) {
        OnPlayer_MovementInput?.Invoke(this, new Vector2InputEventArgs{ 
            inputValue = ctx.ReadValue<Vector2>()
        });
    }

    private void Interact_performed(InputAction.CallbackContext obj) {
        OnPlayer_InteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_canceled(InputAction.CallbackContext obj) {
        OnPlayer_InteractCanceled?.Invoke(this, EventArgs.Empty);
    }

    //Menu Map
    public void EnableMenuInput() {
        gameControls.Menu.Enable();
    }

    public void DisableMenuInput() {
        gameControls.Menu.Disable();
    }

    private void Up_performed(InputAction.CallbackContext obj) {
        OnMenu_UpPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Down_performed(InputAction.CallbackContext obj) {
        OnMenu_DownPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Left_performed(InputAction.CallbackContext obj) {
        OnMenu_LeftPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Right_performed(InputAction.CallbackContext obj) {
        OnMenu_RightPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Accept_performed(InputAction.CallbackContext obj) {
        OnMenu_AcceptPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Cancel_performed(InputAction.CallbackContext obj) {
        OnMenu_CancelPerformed?.Invoke(this, EventArgs.Empty);
    }
}
