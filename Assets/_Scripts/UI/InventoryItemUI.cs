using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : BaseItemUI
{
    public event EventHandler<InventoryItemEventArgs> OnItemUsed;
    public class InventoryItemEventArgs : EventArgs {
        public int itemIndex;
        public ItemSO item;
    }

    private int index;

    public void SetIndex(int index) {
        this.index = index;
    }

    public void UsetItem() {
        if (item.Use()) {
            OnItemUsed?.Invoke(this, new InventoryItemEventArgs {
                itemIndex = index,
                item = item,
            });
        }
    }
}
