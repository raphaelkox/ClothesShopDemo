using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitController : MonoBehaviour
{
    public static PlayerOutfitController Instance;

    [SerializeField] private SpriteRenderer spriteRenderer;

    MaterialPropertyBlock materialBlock;

    private ClothingItemData currentOutfit;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
         materialBlock = new MaterialPropertyBlock();
    }

    public void SetOutift(ClothingItemData clothingItem) {
        if(currentOutfit != null) {
            currentOutfit.Unnequip();
        }

        currentOutfit = clothingItem;
        materialBlock.SetTexture("_ClothesTex", clothingItem.OutfitTextureSheet);
        spriteRenderer.SetPropertyBlock(materialBlock);
    }

    public void RemoveOutfit() {
        currentOutfit = null;
        materialBlock.SetTexture("_ClothesTex", Texture2D.blackTexture);
        spriteRenderer.SetPropertyBlock(materialBlock);
    }
}
