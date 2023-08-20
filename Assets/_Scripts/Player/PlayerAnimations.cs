using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start() {
        playerMovement.OnDirectionXChanged += PlayerMovement_OnDirectionXChanged;
        playerMovement.OnDirectionYChanged += PlayerMovement_OnDirectionYChanged;
    }

    private void Update() {
        animator.SetBool("isMoving", playerMovement.HasMovementInput());
    }

    private void PlayerMovement_OnDirectionYChanged(object sender, PlayerMovement.DirectionEventArgs e) {
        animator.SetFloat("directionY", e.directionValue);
    }

    private void PlayerMovement_OnDirectionXChanged(object sender, PlayerMovement.DirectionEventArgs e) {
        animator.SetFloat("directionX", e.directionValue);
    }
}
