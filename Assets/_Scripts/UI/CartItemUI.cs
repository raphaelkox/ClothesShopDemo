using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItemUI : ShopItemUI
{
    public event EventHandler<CartItemEventArgs> OnItemRemoved;
    public class CartItemEventArgs : EventArgs {
        public int ItemIndex;
    }

    private int index;
    private ShopCartSubWindow parentCartWindow;

    public void SetParentWindow(ShopCartSubWindow parentWindow) {
        this.parentCartWindow = parentWindow;
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
