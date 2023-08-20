using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item List")]
public class ShopItemListSO : ScriptableObject
{
    public List<ItemSO> Items;

    private List<ItemData> itemsData = new List<ItemData>();
    public List<ItemData> ItemsData {
        get {
            if (itemsData.Count <= 0) {
                itemsData = new List<ItemData>();

                foreach (var item in Items)
                {
                    itemsData.Add(ItemDataFactory.GetItemData(item));
                }
            }
            return itemsData;
        } private set { }
    }
}
