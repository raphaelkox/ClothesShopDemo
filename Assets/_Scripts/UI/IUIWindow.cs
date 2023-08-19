using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIWindow
{
    public event EventHandler<WindowEventArgs> OnWindowClose;
    public class WindowEventArgs : EventArgs {
        public IUIWindow Window;
    }

    void Show();
    void Hide();
    void OnAcceptInput();
    void OnCancelInput();
    void OnUpInput();
    void OnDownInput();
    void OnLeftInput();
    void OnRightInput();
}

public interface IUIShopWindow : IUIWindow
{
    void PopulateItems(ShopItemListSO itemList);
}
