using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemUI : BaseItemUI
{
    public event EventHandler<InventoryItemEventArgs> OnItemUsed;
    public class InventoryItemEventArgs : EventArgs {
        public int itemIndex;
        public ItemData item;
    }

    [SerializeField] private GameObject equippedIcon;

    private int index;

    public override void SetItem(ItemData item) {
        this.item = item;
        iconObject.sprite = item.Icon;

        item.OnItemUnnequipped += Item_OnItemUnnequipped;
        item.OnItemEquipped += Item_OnItemEquipped;
    }

    private void Item_OnItemEquipped(object sender, EventArgs e) {
        ShowEquippedIcon();
    }

    private void Item_OnItemUnnequipped(object sender, EventArgs e) {
        HideEquippedIcon();
    }

    public void SetIndex(int index) {
        this.index = index;
    }


    public override void ResetSlot() {
        base.ResetSlot();

        item.OnItemUnnequipped -= Item_OnItemUnnequipped;
        item.OnItemEquipped -= Item_OnItemEquipped;
    }
    public void UsetItem() {
        UISoundEffects.Instance.PlayClickSfx();

        if (item.Use()) {
            OnItemUsed?.Invoke(this, new InventoryItemEventArgs {
                itemIndex = index,
                item = item,
            });
        }
    }    

    public void ShowEquippedIcon() {
        equippedIcon.SetActive(true);
    }

    public void HideEquippedIcon() {
        equippedIcon.SetActive(false);
    }
}
