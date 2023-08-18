using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event EventHandler OnSeleted;
    public event EventHandler OnUnseleted;
    public event EventHandler OnInteract;

    [SerializeField] private bool once;

    private bool blocked;
    private bool used;

    public void Select() {
        OnSeleted?.Invoke(this, EventArgs.Empty);
    }

    public void Unselect() {
        OnUnseleted?.Invoke(this, EventArgs.Empty);
    }

    public void Interact() {
        if(!blocked && (!once || !used)) {
            OnInteract?.Invoke(this, EventArgs.Empty);

            used = true;

            if (once) blocked = true;
            Debug.Log("Interaction Activated!");
            return;
        }
        Debug.Log("Interaction tried to activate, but was blocked!");
    }
}
