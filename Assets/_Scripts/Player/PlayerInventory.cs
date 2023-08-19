using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler<InventoryItemsEventArgs> OnItemsAdded;
    public event EventHandler<InventoryItemsEventArgs> OnItemsRemoved;
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

    public List<ItemSO> GetItems() {
        return itemList;
    }

    public void BuyItems(List<ItemSO> items, float price) {
        AddItems(items);
        RemoveMoney(price);
    }

    public void SellItems(List<ItemSO> items, float price) {
        RemoveItems(items);
        AddMoney(price);
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

    public void RemoveItems(List<ItemSO> itemsToDelete) {
        foreach (var item in itemsToDelete)
        {
            itemList.Remove(item);
        }

        OnItemsRemoved?.Invoke(this, new InventoryItemsEventArgs {
            ItemList = itemsToDelete
        });
    }

    public bool HasEnoughMoney(float amount) {
        return money >= amount;
    }

    public void AddMoney(float valueToAdd) {
        money += valueToAdd;

        OnMoneyChanged?.Invoke(this, new InventoryMoneyEventArgs {
            ValueChange = valueToAdd,
            CurrentValue = money
        });
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
