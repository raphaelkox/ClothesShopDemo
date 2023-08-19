using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveSpeed;

    private Vector2 directionInput;

    // Start is called before the first frame update
    void Start()
    {
        playerControl.OnPlayer_MovementInput += PlayerControl_OnMovementInput;        
    }

    private void PlayerControl_OnMovementInput(object sender, PlayerControl.Vector2InputEventArgs e) {
        directionInput = e.inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = directionInput * moveSpeed;
    }
}
