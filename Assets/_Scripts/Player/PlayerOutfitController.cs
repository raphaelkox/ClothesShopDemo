using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitController : MonoBehaviour
{
    public static PlayerOutfitController Instance;

    [SerializeField] private SpriteRenderer spriteRenderer;

    MaterialPropertyBlock materialBlock;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
         materialBlock = new MaterialPropertyBlock();
    }

    public void SetOutift(ClothingItemSO clothingItem) {
        materialBlock.SetTexture("_ClothesTex", clothingItem.OutfitTextureSheet);
        spriteRenderer.SetPropertyBlock(materialBlock);
    }
}
