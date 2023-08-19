using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryWindow : MonoBehaviour, IUIWindow
{
    public event EventHandler<IUIWindow.WindowEventArgs> OnWindowClose;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private InventoryItemUI templateItemSlot;

    private void Start() {
        playerInventory.OnItemsAdded += PlayerInventory_OnItemsAdded;
        //Hide();
    }

    private void PlayerInventory_OnItemsAdded(object sender, PlayerInventory.InventoryItemsEventArgs e) {
        foreach (var item in e.ItemList)
        {
            AddItemSlot(item);
        }
    }

    private void AddItemSlot(ItemSO item) {
        InventoryItemUI itemSlot;

        int itemCount = playerInventory.GetItemCount();
        
        if (itemCount >= containerTransform.childCount) {
            itemSlot = Instantiate(templateItemSlot, containerTransform);
        } else {
            //slots free for reuse
            itemSlot = containerTransform.GetChild(itemCount).GetComponent<InventoryItemUI>();
        }

        itemSlot.SetItem(item);
        itemSlot.SetIndex(itemCount);
        itemSlot.gameObject.SetActive(true);        
    }

    private void CloseWindow() {
        Hide();
        PlayerControl.Instance.DisableMenuInput();
        PlayerControl.Instance.EnablePlayerInput();
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
