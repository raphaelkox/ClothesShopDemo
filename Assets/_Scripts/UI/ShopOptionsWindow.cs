using AYellowpaper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionsWindow : MonoBehaviour, IUIWindow
{
    public event EventHandler<IUIWindow.WindowEventArgs> OnWindowClose;
    public event EventHandler OnBuyClick;
    public event EventHandler OnSellClick;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;    

    private void Start() {        
        buyButton.onClick.AddListener(BuyClickHandler);
        sellButton.onClick.AddListener(SellClickHandler);
    }

    private void BuyClickHandler() {
        Hide();
        OnBuyClick?.Invoke(this, EventArgs.Empty);        
    }

    private void SellClickHandler() {
        Hide();
        OnSellClick?.Invoke(this, EventArgs.Empty);
    }

    #region IUIWindow Implementation
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnAcceptInput() {
    }

    public void OnCancelInput() {
        Hide();
        OnWindowClose?.Invoke(this, new IUIWindow.WindowEventArgs {
            Window = this
        });
    }

    public void OnDownInput() {
    }

    public void OnLeftInput() {
    }

    public void OnRightInput() {
    }

    public void OnUpInput() {
    }
    #endregion IUIWindow Implementation
}
