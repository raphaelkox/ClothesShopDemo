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

    public static ItemData GetItemData(ItemSO itemSO) {
        var itemData = new ItemData {
            DisplayName = itemSO.DisplayName,
            Description = itemSO.Description,
            Icon = itemSO.Icon,
            Price = itemSO.Price,
        };

        return itemData;
    }

    public static ItemData CloneItemData(ItemData itemData) {
        var itemDataClone = new ItemData {
            DisplayName = itemData.DisplayName,
            Description = itemData.Description,
            Icon = itemData.Icon,
            Price = itemData.Price,
            Equipped = itemData.Equipped,
            Blocked = itemData.Blocked
        };

        return itemDataClone;
    }
}