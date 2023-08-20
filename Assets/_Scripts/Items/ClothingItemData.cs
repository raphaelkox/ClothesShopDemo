using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClothingItemData : ItemData {
    public Texture2D OutfitTextureSheet;

    public override bool Use() {
        if (Equipped) {
            Unnequip();
            return true;
        }

        Equip();
        return true;
    }

    public void Equip() {
        PlayerOutfitController.Instance.SetOutift(this);
        Equipped = true;
        TriggerOnItemEquipped();
    }

    public void Unnequip() {
        PlayerOutfitController.Instance.RemoveOutfit();
        Equipped = false;
        TriggerOnItemUnnequipped();
    }
}
