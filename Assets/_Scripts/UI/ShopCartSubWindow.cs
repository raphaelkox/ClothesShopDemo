using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopCartSubWindow : MonoBehaviour
{
    [SerializeField] private Transform containerTransform;
    [SerializeField] private CartItemUI templateItemSlot;
    [SerializeField] private TextMeshProUGUI subTotalTextObject;

    public List<ShopItemSO> cartItems = new List<ShopItemSO>();

    private float subTotal;

    public void Clear() {
        cartItems.Clear();

        foreach (Transform child in containerTransform) {
            child.gameObject.SetActive(false);
        }
    }

    public void AddItem(ShopItemSO item) {
        CartItemUI itemSlot;

        //slots free for reuse
        if (cartItems.Count >= containerTransform.childCount) {
            itemSlot = Instantiate(templateItemSlot, containerTransform);
            itemSlot.SetParentWindow(this);
        } else { 
            itemSlot = containerTransform.GetChild(cartItems.Count).GetComponent<CartItemUI>();            
        }

        itemSlot.SetItem(item);
        itemSlot.SetIndex(cartItems.Count);
        itemSlot.gameObject.SetActive(true);

        AddItemToList(item);
    }    

    public void RemoveItem(object sender, CartItemUI.CartItemEventArgs e) {
        int index = e.ItemIndex;        
        containerTransform.GetChild(index).gameObject.SetActive(false);
        containerTransform.GetChild(index).SetAsLastSibling();

        RemoveItemFromList(index);
    }

    private void AddItemToList(ShopItemSO item) {
        cartItems.Add(item);

        subTotal += item.Price;
        subTotalTextObject.text = subTotal.ToString("F2");

        UpdateItemIndexes();
    }

    private void RemoveItemFromList(int index) {
        ShopItemSO item = cartItems[index];

        subTotal -= item.Price;
        subTotalTextObject.text = subTotal.ToString("F2");

        cartItems.RemoveAt(index);

        UpdateItemIndexes();
    }

    private void UpdateItemIndexes() {
        for (int i = 0; i < containerTransform.childCount; i++) {
            containerTransform.GetChild(i).GetComponent<CartItemUI>().SetIndex(i);
        }
    }
}
