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
    public event EventHandler OnPlayer_InventoryOpenPerformed;
    public event EventHandler OnPlayer_InventoryOpenCanceled;
    //Menu Map
    public event EventHandler OnMenu_UpPerformed;
    public event EventHandler OnMenu_DownPerformed;
    public event EventHandler OnMenu_LeftPerformed;
    public event EventHandler OnMenu_RightPerformed;
    public event EventHandler OnMenu_AcceptPerformed;
    public event EventHandler OnMenu_CancelPerformed;
    public event EventHandler OnMenu_InventoryClosePerformed;
    public event EventHandler OnMenu_InventoryCloseCanceled;

    private GameControls gameControls;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        gameControls = new GameControls();

        //Player Map
        gameControls.Player.Movement.performed += Player_Movement_updated;
        gameControls.Player.Movement.canceled += Player_Movement_updated;

        gameControls.Player.Interact.performed += Player_Interact_performed;
        gameControls.Player.Interact.canceled += Player_Interact_canceled;

        gameControls.Player.InventoryOpen.performed += Player_InventoryOpen_performed;
        gameControls.Player.InventoryOpen.canceled += Player_InventoryOpen_canceled;

        gameControls.Player.Enable();

        gameControls.Menu.Up.performed += Menu_Up_performed;
        gameControls.Menu.Down.performed += Menu_Down_performed;
        gameControls.Menu.Left.performed += Menu_Left_performed;
        gameControls.Menu.Right.performed += Menu_Right_performed;
        gameControls.Menu.Accept.performed += Menu_Accept_performed;
        gameControls.Menu.Cancel.performed += Menu_Cancel_performed;
        gameControls.Menu.InventoryClose.performed += InventoryClose_performed;
        gameControls.Menu.InventoryClose.canceled += InventoryClose_canceled;

        gameControls.Menu.Disable();
    }    

    //Player Map
    public void EnablePlayerInput() {
        gameControls.Player.Enable();
    }

    public void DisablePlayerInput() {
        gameControls.Player.Disable();
    }

    private void Player_Movement_updated(InputAction.CallbackContext ctx) {
        OnPlayer_MovementInput?.Invoke(this, new Vector2InputEventArgs{ 
            inputValue = ctx.ReadValue<Vector2>()
        });
    }

    private void Player_Interact_performed(InputAction.CallbackContext obj) {
        OnPlayer_InteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Player_Interact_canceled(InputAction.CallbackContext obj) {
        OnPlayer_InteractCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Player_InventoryOpen_canceled(InputAction.CallbackContext obj) {
        OnPlayer_InventoryOpenCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Player_InventoryOpen_performed(InputAction.CallbackContext obj) {
        OnPlayer_InventoryOpenPerformed?.Invoke(this, EventArgs.Empty);
    }

    //Menu Map
    public void EnableMenuInput() {
        gameControls.Menu.Enable();
    }

    public void DisableMenuInput() {
        gameControls.Menu.Disable();
    }

    private void Menu_Up_performed(InputAction.CallbackContext obj) {
        OnMenu_UpPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Menu_Down_performed(InputAction.CallbackContext obj) {
        OnMenu_DownPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Menu_Left_performed(InputAction.CallbackContext obj) {
        OnMenu_LeftPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Menu_Right_performed(InputAction.CallbackContext obj) {
        OnMenu_RightPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Menu_Accept_performed(InputAction.CallbackContext obj) {
        OnMenu_AcceptPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Menu_Cancel_performed(InputAction.CallbackContext obj) {
        OnMenu_CancelPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void InventoryClose_canceled(InputAction.CallbackContext obj) {
        OnMenu_InventoryCloseCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void InventoryClose_performed(InputAction.CallbackContext obj) {
        OnMenu_InventoryClosePerformed?.Invoke(this, EventArgs.Empty);
    }
}
