using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public static PlayerInteractionController Instance;

    [SerializeField] private PlayerControl playerControl;

    private List<IPlayerInteraction> interactionsInRange = new List<IPlayerInteraction>();
    private IPlayerInteraction selectedInteraction;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        playerControl.OnPlayer_InteractPerformed += PlayerControl_OnInteractPerformed;
    }    

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out IPlayerInteraction interaction)) {
            AddInteraction(interaction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out IPlayerInteraction interaction)) {
            RemoveInteraction(interaction);
        }
    }

    public void AddInteraction(IPlayerInteraction interaction) {
        interactionsInRange.Add(interaction);
        SelectInteraction(interaction);
    }

    public void RemoveInteraction(IPlayerInteraction interaction) {
        if (interaction == selectedInteraction) {
            interaction.Unselect();
            selectedInteraction = null;
        }

        interactionsInRange.Remove(interaction);
    }

    public void SelectInteraction(IPlayerInteraction interaction) {
        selectedInteraction?.Unselect();

        if (selectedInteraction == null || (interaction != selectedInteraction)) {
            selectedInteraction = interaction;
            interaction.Select();
        }
    }

    private void PlayerControl_OnInteractPerformed(object sender, System.EventArgs e) {
        selectedInteraction?.Interact();
    }
}
