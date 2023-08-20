using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClone : MonoBehaviour
{
    [SerializeField] ItemSO item;
    [SerializeField] ItemSO clothingItem;

    private ItemData itemData;
    private ItemData clothingItemData;

    private void Start() {
        itemData = ItemDataFactory.GetItemData(item);
        clothingItemData = ItemDataFactory.GetItemData(clothingItem);

        var x = 0;
    }
}
