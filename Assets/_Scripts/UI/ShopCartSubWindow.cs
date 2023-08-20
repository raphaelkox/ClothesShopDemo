using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopCartSubWindow : MonoBehaviour
{
    public event EventHandler<CartWindowActionEventArgs> OnCartAction;
    public class CartWindowActionEventArgs {
        public List<ItemSO> ItemList;
        public List<int> ItemIndexes;
        public float MoneyAmount;
    }

    [SerializeField] private Transform containerTransform;
    [SerializeField] private CartItemUI templateItemSlot;
    [SerializeField] private TextMeshProUGUI subTotalTextObject;
    [SerializeField] private Color subTotalOkTextColor = Color.white;
    [SerializeField] private Color subTotalBlockedTextColor = Color.white;
    [SerializeField] private Button actionButton;

    public List<ItemSO> cartItems = new List<ItemSO>();

    private float subTotal;
    private bool ignoreMoneyAmount;

    private void Start() {
        actionButton.onClick.AddListener(OnActionClick);
    }

    private void OnActionClick() {       
        OnCartAction?.Invoke(this, new CartWindowActionEventArgs {
            ItemList = cartItems,
            MoneyAmount = subTotal
        });

        Clear();
    }

    public void Clear() {
        cartItems.Clear();
        subTotal = 0f;
        UpdateSubtotal();
        SetActionState(false);

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

        if (ignoreMoneyAmount && cartItems.Count > 0) {
            SetActionState(true);
            return;
        }

        bool playerHasEnoughMoney = PlayerInventory.Instance.HasEnoughMoney(subTotal);
        subTotalTextObject.color = playerHasEnoughMoney ? subTotalOkTextColor : subTotalBlockedTextColor;
        SetActionState(playerHasEnoughMoney && cartItems.Count > 0);
    }

    public void SetActionState(bool newState) {
        actionButton.interactable = newState;
    }

    public void SetIgnoreMoneyFlag(bool newState) { 
        ignoreMoneyAmount = newState;
    }
}
