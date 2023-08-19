using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : MonoBehaviour, IUIWindow
{
    public event EventHandler<IUIWindow.WindowEventArgs> OnWindowClose;

    [SerializeField] private WindowTitleBarUI windowTitleBar;
    [SerializeField] private WindowTitleBarUI cartTitleBar;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private ShopItemUI templateItemSlot;
    [SerializeField] private Button closeButton;
    [SerializeField] private ShopCartSubWindow cartSubWindow;

    private EventHandler<ShopCartSubWindow.CartWindowActionEventArgs> windowCartAction;

    private void Start() {
        closeButton.onClick.AddListener(CloseWindow);
        cartSubWindow.OnCartAction += CartSubWindow_OnCartAction;
    }

    public void SetTitleBar(string titleText, Color titleBarColor) {
        windowTitleBar.SetTitleText(titleText);
        windowTitleBar.SetTitleBarColor(titleBarColor);
        cartTitleBar.SetTitleBarColor(titleBarColor);
    }

    public void RegisterWindowCartAction(EventHandler<ShopCartSubWindow.CartWindowActionEventArgs> callback) {
        windowCartAction = callback;
    }

    public void UnregisterWindowCartAction() {
        windowCartAction = null;
    }

    private void CartSubWindow_OnCartAction(object sender, ShopCartSubWindow.CartWindowActionEventArgs e) {
        windowCartAction.Invoke(this, e);
    }

    private void CloseWindow() {
        Hide();
        cartSubWindow.Clear();
        OnWindowClose?.Invoke(this, new IUIWindow.WindowEventArgs {
            Window = this
        });
    }

    public void PopulateItems(List<ItemSO> itemList) {
        if(itemList.Count > containerTransform.childCount) {
            //iterate over available slots
            for (int i = 0; i < containerTransform.childCount; i++) {
                if(containerTransform.GetChild(i).TryGetComponent(out ShopItemUI itemUI)){
                    itemUI.SetItem(itemList[i]);
                    itemUI.gameObject.SetActive(true);
                }
            }

            //create new slots
            for (int i = containerTransform.childCount; i < itemList.Count; i++) {
                ShopItemUI newItemUI = Instantiate(templateItemSlot, containerTransform);
                newItemUI.SetItem(itemList[i]);
                newItemUI.gameObject.SetActive(true);
                newItemUI.SetParentWindow(this);
            }
        }
        else {
            //fill needed slots
            for (int i = 0; i < itemList.Count; i++) {
                if (containerTransform.GetChild(i).TryGetComponent(out ShopItemUI itemUI)) { 
                    itemUI.SetItem(itemList[i]);
                    itemUI.gameObject.SetActive(true);
                }
            }

            //hide remaining slots to be reused later
            for (int i = itemList.Count; i < containerTransform.childCount; i++) {
                containerTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void AddItemToCart(object sender, ShopItemUI.ShopItemEventArgs e) {
        cartSubWindow.AddItem(e.Item);
    }

    #region IUIWindow Implementation
    public void Show() {
        gameObject.SetActive(true);
        cartSubWindow.SetActionState(false);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnAcceptInput() {
    }

    public void OnCancelInput() {
        CloseWindow();
    }

    public void OnDownInput() {
    }

    public void OnLeftInput() {
    }

    public void OnRightInput() {
    }

    public void OnUpInput() {
    }
    
    #endregion IUIWindow Implementation 
}
