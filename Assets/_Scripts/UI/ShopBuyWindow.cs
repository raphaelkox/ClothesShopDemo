using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ShopItemUI;
using static UnityEditor.Progress;

public class ShopBuyWindow : MonoBehaviour, IUIShopWindow
{
    public event EventHandler<IUIWindow.WindowEventArgs> OnWindowClose;

    [SerializeField] private Transform containerTransform;
    [SerializeField] private ShopItemUI templateItemSlot;
    [SerializeField] private Button closeButton;
    [SerializeField] private ShopCartSubWindow cartSubWindow;

    private void Start() {
        closeButton.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow() {
        Hide();
        cartSubWindow.Clear();
        OnWindowClose?.Invoke(this, new IUIWindow.WindowEventArgs {
            Window = this
        });
    }

    public void PopulateItems(ShopItemListSO itemList) {
        if(itemList.Items.Count > containerTransform.childCount) {
            //iterate over available slots
            for (int i = 0; i < containerTransform.childCount; i++) {
                if(containerTransform.GetChild(i).TryGetComponent(out ShopItemUI itemUI)){
                    itemUI.SetItem(itemList.Items[i]);  
                }
            }

            //create new slots
            for (int i = containerTransform.childCount; i < itemList.Items.Count; i++) {
                ShopItemUI newItemUI = Instantiate(templateItemSlot, containerTransform);
                newItemUI.SetItem(itemList.Items[i]);
                newItemUI.gameObject.SetActive(true);
                newItemUI.SetParentWindow(this);
            }
        }
        else {
            //fill needed slots
            for (int i = 0; i < itemList.Items.Count; i++) {
                if (containerTransform.GetChild(i).TryGetComponent(out ShopItemUI itemUI)) { 
                    itemUI.SetItem(itemList.Items[i]);
                }
            }

            //hide remaining slots to be reused later
            for (int i = itemList.Items.Count; i < containerTransform.childCount; i++) {
                containerTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void AddItemToCart(object sender, ShopItemEventArgs e) {
        cartSubWindow.AddItem(e.Item);
    }

    #region IUIWindow Implementation
    public void Show() {
        gameObject.SetActive(true);
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
