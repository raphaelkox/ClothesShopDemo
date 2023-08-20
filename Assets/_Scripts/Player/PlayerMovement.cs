using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler<DirectionEventArgs> OnDirectionXChanged;
    public event EventHandler<DirectionEventArgs> OnDirectionYChanged;
    public class DirectionEventArgs : EventArgs {
        public float directionValue;
    }

    [SerializeField] private PlayerControl playerControl;    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveSpeed;

    private Vector2 facingDirection;
    private Vector2 directionInput;

    // Start is called before the first frame update
    void Start()
    {
        playerControl.OnPlayer_MovementInput += PlayerControl_OnMovementInput;        
    }

    private void PlayerControl_OnMovementInput(object sender, PlayerControl.Vector2InputEventArgs e) {
        directionInput = e.inputValue;

        UpdateFacingDirection();
    }

    private void UpdateFacingDirection() {
        if (directionInput == Vector2.zero) return;

        facingDirection.x = directionInput.x;
        OnDirectionXChanged?.Invoke(this, new DirectionEventArgs {
            directionValue = facingDirection.x,
        });

        facingDirection.y = directionInput.y;
        OnDirectionYChanged?.Invoke(this, new DirectionEventArgs {
            directionValue = facingDirection.y,
        });
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = directionInput * moveSpeed;
    }

    public bool HasMovementInput() {
        return directionInput != Vector2.zero;
    }
}