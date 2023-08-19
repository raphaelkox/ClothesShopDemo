using AYellowpaper;
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
    [SerializeField] private InterfaceReference<IUIShopWindow> buyWindow;
    [SerializeField] private InterfaceReference<IUIShopWindow> sellWindow;
    [SerializeField] private ShopItemListSO shopItemList;

    private bool interactionBlocked;

    private void Start() {
        HideObjects();
        RegisterEvents();
    }

    private void HideObjects() {
        interactionIcon.enabled = false;
        shopOptionsWindow.Hide();
        buyWindow.Value.Hide();
    }

    private void RegisterEvents() {
        shopOptionsWindow.OnBuyClick += ShopOptionsWindow_OnBuyClick;
        shopOptionsWindow.OnSellClick += ShopOptionsWindow_OnSellClick;        
    }

    private void ShopOptionsWindow_OnBuyClick(object sender, EventArgs e) {
        buyWindow.Value.PopulateItems(shopItemList);
        UIWindowStack.Instance.PushWindow(buyWindow.Value);
    }

    private void ShopOptionsWindow_OnSellClick(object sender, EventArgs e) {
        
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
