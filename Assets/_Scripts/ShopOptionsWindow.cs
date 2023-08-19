using AYellowpaper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionsWindow : MonoBehaviour, IUIWindow
{
    public event EventHandler<IUIWindow.WindowEventArgs> OnWindowClose;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;

    [SerializeField] private InterfaceReference<IUIWindow> buyWindow;
    [SerializeField] private InterfaceReference<IUIWindow> sellWindow;

    private void Start() {
        buyWindow.Value.Hide();
        buyButton.onClick.AddListener(BuyClickHandler);
        sellButton.onClick.AddListener(SellClickHandler);
    }

    private void BuyClickHandler() {
        Hide();
        buyWindow.Value.Show();
        UIWindowStack.Instance.PushWindow(buyWindow.Value);
    }

    private void SellClickHandler() {
        Hide();
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
