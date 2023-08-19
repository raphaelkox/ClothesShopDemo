using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item List")]
public class ShopItemListSO : ScriptableObject
{
    public List<ShopItemSO> Items;
}
