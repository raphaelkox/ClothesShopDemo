using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public event EventHandler<ShopItemEventArgs> OnAddItemToCart;
    public class ShopItemEventArgs : EventArgs {
        public ShopItemSO Item;
    }

    [SerializeField] ShopItemSO item;
    [SerializeField] Image iconObject;
    [SerializeField] TextMeshProUGUI priceTextObject;

    private ShopBuyWindow parentBuyWindow;

    public void SetItem(ShopItemSO item) {
        this.item = item;

        iconObject.sprite = item.Icon;
        priceTextObject.text = item.Price.ToString("F2");
    }

    public void SetParentWindow(ShopBuyWindow parentWindow) {
        if (parentBuyWindow != null) return;
        this.parentBuyWindow = parentWindow;
        OnAddItemToCart += parentWindow.AddItemToCart;
    }

    public void AddToCart() {
        OnAddItemToCart?.Invoke(this, new ShopItemEventArgs { Item = item });
    }
}
