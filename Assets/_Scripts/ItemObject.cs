using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData {
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public float Price;

    public Func<bool> UseFunction;
}

[Serializable]
public class ClothingItemData : ItemData {
    public Texture2D OutfitTextureSheet;
}
