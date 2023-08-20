using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item List")]
public class ShopItemListSO : ScriptableObject
{
    public List<ItemSO> Items;

    public List<ItemData> GetItemsData() {
        List<ItemData> itemsData = new();

        foreach (var item in Items) {
            itemsData.Add(ItemDataFactory.GetItemData(item));
        }

        return itemsData;
    }    
}
