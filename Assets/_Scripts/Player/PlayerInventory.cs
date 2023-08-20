using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event EventHandler<InventoryItemsEventArgs> OnItemsAdded;
    public event EventHandler<InventoryItemsEventArgs> OnItemsRemoved;
    public class InventoryItemsEventArgs : EventArgs {
        public List<ItemData> ItemList;
    }

    public event EventHandler<InventoryMoneyEventArgs> OnMoneyChanged;
    public class InventoryMoneyEventArgs : EventArgs {
        public float ValueChange;
        public float CurrentValue;
    }

    public static PlayerInventory Instance;

    [SerializeField] private TextMeshProUGUI moneyTextObject;
    [SerializeField] private float startingMoney;

    private List<ItemData> itemList = new List<ItemData>();
    private float money;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        AddMoney(startingMoney);
    }

    public List<ItemData> GetItems() {
        return itemList;
    }

    public void BuyItems(List<ItemData> items, float price) {
        AddItems(items);
        RemoveMoney(price);
    }

    public void SellItems(List<ItemData> items, float price) {
        RemoveItems(items);
        AddMoney(price);
    }

    public void AddItem(ItemData item) {
        AddItems(new List<ItemData> { item });
    }

    public void AddItems(List<ItemData> items) {
        List<ItemData> newItems = new List<ItemData>();

        foreach (var item in items)
        {
            newItems.Add(ItemDataFactory.CloneItemData(item));
        }

        itemList.AddRange(newItems);
        OnItemsAdded?.Invoke(this, new InventoryItemsEventArgs {
            ItemList = newItems
        });
    }

    public void RemoveItems(List<ItemData> itemsToDelete) {
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
        UpdateMoneyUI();
        OnMoneyChanged?.Invoke(this, new InventoryMoneyEventArgs {
            ValueChange = valueToAdd,
            CurrentValue = money
        });
    }

    public bool RemoveMoney(float valueToRemove) {
        if (money < valueToRemove) return false;

        money -= valueToRemove;
        UpdateMoneyUI();
        OnMoneyChanged?.Invoke(this, new InventoryMoneyEventArgs {
            ValueChange = valueToRemove,
            CurrentValue = money
        });
        return true;
    }

    private void UpdateMoneyUI() {
        moneyTextObject.text = money.ToString("F2");
    }

    public int GetItemCount() {
        return itemList.Count;
    }
}
