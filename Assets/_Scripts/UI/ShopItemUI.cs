using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : BaseItemUI
{
    public event EventHandler<ShopItemEventArgs> OnAddItemToCart;
    public class ShopItemEventArgs : EventArgs {
        public ItemData Item;
    }

    [SerializeField] protected Button itemButton;
    [SerializeField] protected TextMeshProUGUI priceTextObject;

    public override void SetItem(ItemData item) {
        base.SetItem(item);

        itemButton.interactable = !item.Equipped;
        priceTextObject.text = item.Price.ToString("F2");
    }

    public void SetParentWindow(ShopWindow parentWindow) {
        OnAddItemToCart += parentWindow.AddItemToCart;
    }

    public void AddToCart() {
        OnAddItemToCart?.Invoke(this, new ShopItemEventArgs { Item = item });
    }
}
