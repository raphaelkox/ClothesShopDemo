using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopCartSubWindow : MonoBehaviour
{
    public event EventHandler OnCartCheckout;

    [SerializeField] private Transform containerTransform;
    [SerializeField] private CartItemUI templateItemSlot;
    [SerializeField] private TextMeshProUGUI subTotalTextObject;
    [SerializeField] private Color subTotalOkTextColor = Color.white;
    [SerializeField] private Color subTotalBlockedTextColor = Color.white;
    [SerializeField] private Button checkoutButton;

    public List<ItemSO> cartItems = new List<ItemSO>();

    private float subTotal;

    private void Start() {
        checkoutButton.onClick.AddListener(OnCheckoutClick);
    }

    private void OnCheckoutClick() {
        PlayerInventory.Instance.BuyItems(cartItems, subTotal);

        OnCartCheckout?.Invoke(this, EventArgs.Empty);

        Clear();
    }

    public void Clear() {
        cartItems.Clear();
        subTotal = 0f;
        UpdateSubtotal();

        foreach (Transform child in containerTransform) {
            child.gameObject.SetActive(false);
        }
    }

    public void AddItem(ItemSO item) {
        CartItemUI itemSlot;
        
        if (cartItems.Count >= containerTransform.childCount) {
            itemSlot = Instantiate(templateItemSlot, containerTransform);
            itemSlot.SetParentWindow(this);
        } else {
            //slots free for reuse
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

    private void AddItemToList(ItemSO item) {
        cartItems.Add(item);

        subTotal += item.Price;
        UpdateSubtotal();

        UpdateItemIndexes();
    }

    private void RemoveItemFromList(int index) {
        ItemSO item = cartItems[index];

        subTotal -= item.Price;
        UpdateSubtotal();

        cartItems.RemoveAt(index);

        UpdateItemIndexes();
    }

    private void UpdateItemIndexes() {
        for (int i = 0; i < containerTransform.childCount; i++) {
            containerTransform.GetChild(i).GetComponent<CartItemUI>().SetIndex(i);
        }
    }

    private void UpdateSubtotal() {
        subTotalTextObject.text = subTotal.ToString("F2");

        bool playerHasEnoughMoney = PlayerInventory.Instance.HasEnoughMoney(subTotal);
        subTotalTextObject.color = playerHasEnoughMoney ? subTotalOkTextColor : subTotalBlockedTextColor;
        checkoutButton.interactable = playerHasEnoughMoney;
    }
}
