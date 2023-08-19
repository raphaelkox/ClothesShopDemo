using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler<InventoryItemsEventArgs> OnItemsAdded;
    public class InventoryItemsEventArgs : EventArgs {
        public List<ItemSO> ItemList;
    }

    public event EventHandler<InventoryMoneyEventArgs> OnMoneyChanged;
    public class InventoryMoneyEventArgs : EventArgs {
        public float ValueChange;
        public float CurrentValue;
    }

    public static PlayerInventory Instance;

    private List<ItemSO> itemList = new List<ItemSO>();
    private float money = 50;

    private void Awake() {
        Instance = this;
    }

    public void BuyItems(List<ItemSO> items, float price) {
        AddItems(items);
        RemoveMoney(price);
    }

    public void AddItem(ItemSO item) {
        AddItems(new List<ItemSO> { item });
    }

    public void AddItems(List<ItemSO> items) {
        itemList.AddRange(items);

        OnItemsAdded?.Invoke(this, new InventoryItemsEventArgs {
            ItemList = items
        });
    }

    public bool HasEnoughMoney(float amount) {
        return money >= amount;
    }

    public bool RemoveMoney(float valueToRemove) {
        if (money < valueToRemove) return false;

        money -= valueToRemove;
        OnMoneyChanged?.Invoke(this, new InventoryMoneyEventArgs {
            ValueChange = valueToRemove,
            CurrentValue = money
        });
        return true;
    }

    public int GetItemCount() {
        return itemList.Count;
    }
}
