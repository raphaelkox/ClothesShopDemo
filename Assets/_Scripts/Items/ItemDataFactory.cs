using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataFactory {
    public static ItemData GetItemData(ItemSO itemSO) {
        if (itemSO is ClothingItemSO) {
            return ClothingItemSO.GetClothingItemData(itemSO as ClothingItemSO);
        }

        return ItemSO.GetItemData(itemSO);
    }

    public static ItemData CloneItemData(ItemData itemData) {
        if (itemData is ClothingItemData) {
            return ClothingItemSO.CloneClothingItemData(itemData as ClothingItemData);
        }

        return ItemSO.CloneItemData(itemData);
    }
}
