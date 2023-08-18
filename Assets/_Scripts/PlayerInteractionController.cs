using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    public List<PlayerInteraction> interactionsInRange;
    private PlayerInteraction selectedInteraction;

    private void Start() {
        playerControl.OnInteractPerformed += PlayerControl_OnInteractPerformed;
    }    

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out PlayerInteraction interaction)) {
            interactionsInRange.Add(interaction);

            SelectInteraction(interaction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out PlayerInteraction interaction)) {
            if(interaction == selectedInteraction) {
                interaction.Unselect();
            }

            interactionsInRange.Remove(interaction);
        }
    }

    private void SelectInteraction(PlayerInteraction interaction) {
        if (interaction != selectedInteraction) {
            selectedInteraction = interaction;
            interaction.Select();
        }
    }

    private void PlayerControl_OnInteractPerformed(object sender, System.EventArgs e) {
        selectedInteraction?.Interact();
    }
}
