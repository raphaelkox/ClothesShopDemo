using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Clothing Item")]
public class ClothingItemSO : ItemSO
{
    public Texture2D OutfitTextureSheet;    

    public static ClothingItemData GetClothingItemData(ClothingItemSO clothingItemSO) {
        var clothingItemData = new ClothingItemData {
            DisplayName = clothingItemSO.DisplayName,
            Description = clothingItemSO.Description,
            Icon = clothingItemSO.Icon,
            Price = clothingItemSO.Price,
            OutfitTextureSheet = clothingItemSO.OutfitTextureSheet
        };

        return clothingItemData;
    }

    public static ClothingItemData CloneClothingItemData(ClothingItemData clothingItemData) {
        var itemDataClone = new ClothingItemData {
            DisplayName = clothingItemData.DisplayName,
            Description = clothingItemData.Description,
            Icon = clothingItemData.Icon,
            Price = clothingItemData.Price,
            OutfitTextureSheet = clothingItemData.OutfitTextureSheet,
            Equipped = clothingItemData.Equipped,
            Blocked = clothingItemData.Blocked
        };

        return itemDataClone;
    }
}