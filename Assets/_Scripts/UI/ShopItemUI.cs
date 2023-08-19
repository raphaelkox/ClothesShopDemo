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
        public ItemSO Item;
    }

    [SerializeField] protected TextMeshProUGUI priceTextObject;

    public override void SetItem(ItemSO item) {
        base.SetItem(item);

        priceTextObject.text = item.Price.ToString("F2");
    }

    public void SetParentWindow(ShopWindow parentWindow) {
        OnAddItemToCart += parentWindow.AddItemToCart;
    }

    public void AddToCart() {
        OnAddItemToCart?.Invoke(this, new ShopItemEventArgs { Item = item });
    }
}
