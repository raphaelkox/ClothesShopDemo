using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItemUI : BaseItemUI
{
    public event EventHandler<CartItemEventArgs> OnItemRemoved;
    public class CartItemEventArgs : EventArgs {
        public int ItemIndex;
    }

    [SerializeField] protected TextMeshProUGUI priceTextObject;

    private int index;

    public override void SetItem(ItemData item) {
        base.SetItem(item);

        priceTextObject.text = item.Price.ToString("F2");
    }

    public void SetParentWindow(ShopCartSubWindow parentWindow) {
        OnItemRemoved += parentWindow.RemoveItem;
    }

    public void SetIndex(int index) {
        this.index = index;
    }

    public void RemoveItem() {
        OnItemRemoved?.Invoke(this, new CartItemEventArgs { 
            ItemIndex = index 
        });
    }
}
