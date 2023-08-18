using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionVisual : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] PlayerInteraction interaction;

    private void Start() {
        interaction.OnSeleted += Interaction_OnSeleted;
        interaction.OnUnseleted += Interaction_OnUnseleted;

        sprite.enabled = false;
    }

    private void Interaction_OnUnseleted(object sender, System.EventArgs e) {
        sprite.enabled = false;
    }

    private void Interaction_OnSeleted(object sender, System.EventArgs e) {
        sprite.enabled = true;
    }
}
