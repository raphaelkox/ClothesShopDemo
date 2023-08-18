using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    public List<PlayerInteraction> interactionsInRange;//private
    public PlayerInteraction selectedInteraction;//private
    public int selectedInteractionIndex; //private

    private void Start() {
        playerControl.OnInteractPerformed += PlayerControl_OnInteractPerformed;
        playerControl.OnSwitchInteractionPerformed += PlayerControl_OnSwitchInteractionPerformed;
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
                UnselectInteraction(interaction);
            }

            interactionsInRange.Remove(interaction);
        }
    }

    private void SelectInteraction(PlayerInteraction interaction) {
        if (interaction != selectedInteraction) {
            if (selectedInteraction) selectedInteraction.Unselect();
            selectedInteraction = interaction;
            selectedInteractionIndex = interactionsInRange.IndexOf(interaction);
            interaction.Select();
        }
    }

    private void UnselectInteraction(PlayerInteraction interaction) {
        interaction.Unselect();

        SelectNextInteraction();
    }

    private void SelectInteraction(int index) {
        if(index != selectedInteractionIndex) {
            if (selectedInteraction) selectedInteraction.Unselect();

            selectedInteractionIndex = index;
            selectedInteraction = interactionsInRange[index];
            selectedInteraction.Select();
        }
    }

    private void SelectNextInteraction() {
        if (interactionsInRange.Count > 0)
        {
            int nextIndex = selectedInteractionIndex + 1;
            nextIndex %= interactionsInRange.Count;

            SelectInteraction(nextIndex);
            return;
        }

        selectedInteractionIndex = -1;
        selectedInteraction = null;
    }

    private void PlayerControl_OnInteractPerformed(object sender, System.EventArgs e) {
        selectedInteraction?.Interact();
    }

    private void PlayerControl_OnSwitchInteractionPerformed(object sender, System.EventArgs e) {
        SelectNextInteraction();
    }
}
