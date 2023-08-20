using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryWindowUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private InventoryItemUI templateItemSlot;
    [SerializeField] private TextMeshProUGUI moneyTextObject;

    private bool state;

    private void Start() {
        playerInventory.OnItemsAdded += PlayerInventory_OnItemsAdded;
        playerInventory.OnItemsRemoved += PlayerInventory_OnItemsRemoved;
        playerInventory.OnMoneyChanged += PlayerInventory_OnMoneyChanged;
        PlayerControl.Instance.OnPlayer_InventoryOpenPerformed += Instance_OnPlayer_InventoryOpenPerformed;
        Hide();
    }

    private void PlayerInventory_OnMoneyChanged(object sender, PlayerInventory.InventoryMoneyEventArgs e) {
        moneyTextObject.text = e.CurrentValue.ToString("F2");
    }

    private void Instance_OnPlayer_InventoryOpenPerformed(object sender, EventArgs e) {
        Toggle();
    }

    public void Toggle() {
        state = !state;

        if (state) {
            Show();
        } else {
            Hide();
        }
    }

    private void PlayerInventory_OnItemsRemoved(object sender, PlayerInventory.InventoryItemsEventArgs e) {
        InventoryItemUI itemUI;
        ItemData itemObj;

        for (int i = containerTransform.childCount - 1; i >= 0; i--) {
            itemUI = containerTransform.GetChild(i).GetComponent<InventoryItemUI>();
            itemObj = itemUI.Getitem();
            if (e.ItemList.Contains(itemObj))
            {
                itemUI.ResetSlot();
                e.ItemList.Remove(itemObj);
            }
        }
    }

    private void PlayerInventory_OnItemsAdded(object sender, PlayerInventory.InventoryItemsEventArgs e) {
        foreach (var item in e.ItemList)
        {
            AddItemSlot(item);
        }
    }

    private void AddItemSlot(ItemData item) {
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

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public InventoryItemUI GetSlot(int index) { 
        if(index < containerTransform.childCount) {
            return containerTransform.GetChild(index).GetComponent<InventoryItemUI>();
        }

        return null;
    }
}
