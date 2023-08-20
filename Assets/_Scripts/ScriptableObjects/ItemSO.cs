using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Base Item")]
public class ItemSO : ScriptableObject
{
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public float Price;

    public virtual bool Use() { return false; }

    public static ItemData GetItemData(ItemSO itemSO) {
        var itemData = new ItemData {
            DisplayName = itemSO.DisplayName,
            Description = itemSO.Description,
            Icon = itemSO.Icon,
            Price = itemSO.Price,
            UseFunction = itemSO.Use
        };

        return itemData;
    }
}