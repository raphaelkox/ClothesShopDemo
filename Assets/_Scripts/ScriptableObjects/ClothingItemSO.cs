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
}
