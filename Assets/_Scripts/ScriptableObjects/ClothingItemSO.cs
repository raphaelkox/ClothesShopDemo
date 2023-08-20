using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Clothing Item")]
public class ClothingItemSO : ItemSO
{
    public Texture2D OutfitTextureSheet;

    public override bool Use() {
        PlayerOutfitController.Instance.SetOutift(this);
        return true;
    }

    public static ClothingItemData GetClothingItemData(ClothingItemSO clothingItemSO) {
        var clothingItemData = new ClothingItemData {
            DisplayName = clothingItemSO.DisplayName,
            Description = clothingItemSO.Description,
            Icon = clothingItemSO.Icon,
            Price = clothingItemSO.Price,
            OutfitTextureSheet = clothingItemSO.OutfitTextureSheet,
            UseFunction = clothingItemSO.Use
        };

        return clothingItemData;
    }
}