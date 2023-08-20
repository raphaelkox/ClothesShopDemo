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
    [SerializeField] private ShopWindow shopWindow;
    [Space]
    [SerializeField] private ShopWindowStyleSO buyWindowStyle;
    [SerializeField] private ShopWindowStyleSO sellWindowStyle;
    [SerializeField] private ShopItemListSO shopItemList;

    private bool interactionBlocked;
    private List<ItemData> shopItemDataList;

    private void Start() {
        shopItemDataList = shopItemList.ItemsData;
        HideObjects();
        RegisterEvents();
    }

    private void BuyItems(object sender, ShopCartSubWindow.CartWindowActionEventArgs e) {
        PlayerInventory.Instance.BuyItems(e.ItemList, e.MoneyAmount);
    }

    private void SellItems(object sender, ShopCartSubWindow.CartWindowActionEventArgs e) {
        PlayerInventory.Instance.SellItems(e.ItemList, e.MoneyAmount);

        //refresh shop view
        shopWindow.PopulateItems(PlayerInventory.Instance.GetItems());
    }

    private void HideObjects() {
        interactionIcon.enabled = false;
        shopOptionsWindow.Hide();
        shopWindow.Hide();
    }

    private void RegisterEvents() {
        shopOptionsWindow.OnBuyClick += ShopOptionsWindow_OnBuyClick;
        shopOptionsWindow.OnSellClick += ShopOptionsWindow_OnSellClick;        
    }

    private void ShopOptionsWindow_OnBuyClick(object sender, EventArgs e) {
        shopWindow.Setup(
            shopItemDataList,
            false,
            BuyItems,
            buyWindowStyle
        );
        UIWindowStack.Instance.PushWindow(shopWindow);
    }

    private void ShopOptionsWindow_OnSellClick(object sender, EventArgs e) {
        shopWindow.Setup(
            PlayerInventory.Instance.GetItems(),
            true,
            SellItems,
            sellWindowStyle
        );
        UIWindowStack.Instance.PushWindow(shopWindow);
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
