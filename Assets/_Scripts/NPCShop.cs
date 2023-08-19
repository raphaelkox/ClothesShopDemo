using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShop : MonoBehaviour, IPlayerInteraction
{
    public event EventHandler OnSelected;
    public event EventHandler OnUnselected;
    public event EventHandler OnInteract;
    public event EventHandler OnInteractEnd;

    [SerializeField] private SpriteRenderer interactionIcon;
    
    [SerializeField] private ShopOptionsWindow shopOptionsWindow;

    private bool interactionBlocked;

    private void Start() {
        interactionIcon.enabled = false;
        shopOptionsWindow.Hide();
    }

    private void ShopOptionsWindow_OnWindowClose(object sender, EventArgs e) {
        InteractEnd();
    }

    #region IPlayerInteraction Implementation
    public void Select() {
        interactionIcon.enabled = true;
        OnSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Unselect() {
        interactionIcon.enabled = false;
        OnUnselected?.Invoke(this, EventArgs.Empty);
    }

    public void Interact() {
        if (interactionBlocked) return;
        interactionBlocked = true;

        PlayerControl.Instance.DisablePlayerInput();
        PlayerControl.Instance.EnableMenuInput();

        shopOptionsWindow.Show();
        shopOptionsWindow.OnWindowClose += ShopOptionsWindow_OnWindowClose;
        UIWindowStack.Instance.PushWindow(shopOptionsWindow);

        OnInteract?.Invoke(this, EventArgs.Empty);
        Unselect();
    }    

    public void InteractEnd() {
        interactionBlocked = false;

        PlayerControl.Instance.DisableMenuInput();
        PlayerControl.Instance.EnablePlayerInput();

        OnInteractEnd?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
