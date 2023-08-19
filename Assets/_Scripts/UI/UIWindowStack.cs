using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIWindowStack : MonoBehaviour
{
    public static UIWindowStack Instance;

    public event EventHandler OnLastWindowClosed;

    private Stack<IUIWindow> windowStack = new Stack<IUIWindow>();
    private IUIWindow currentWindow;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        PlayerControl.Instance.OnMenu_AcceptPerformed += Instance_OnMenu_AcceptPerformed;
        PlayerControl.Instance.OnMenu_CancelPerformed += Instance_OnMenu_CancelPerformed;
        PlayerControl.Instance.OnMenu_UpPerformed += Instance_OnMenu_UpPerformed;
        PlayerControl.Instance.OnMenu_DownPerformed += Instance_OnMenu_DownPerformed;
        PlayerControl.Instance.OnMenu_LeftPerformed += Instance_OnMenu_LeftPerformed;
        PlayerControl.Instance.OnMenu_RightPerformed += Instance_OnMenu_RightPerformed;
    }

    public void PushWindow(IUIWindow window) {
        if (currentWindow != null) {
            currentWindow.OnWindowClose -= Window_OnWindowClose;
            windowStack.Push(currentWindow);
        }

        SetCurrentWindow(window);
    }

    private void SetCurrentWindow(IUIWindow window) {
        currentWindow = window;
        window.OnWindowClose += Window_OnWindowClose;
        window.Show();
    }

    private void Window_OnWindowClose(object sender, IUIWindow.WindowEventArgs e) {
        e.Window.OnWindowClose -= Window_OnWindowClose;

        if (windowStack.Count > 0) {
            SetCurrentWindow(windowStack.Pop());
            return;
        }

        windowStack.Clear();
        currentWindow = null;
        OnLastWindowClosed?.Invoke(this, EventArgs.Empty);
    }

    private void Instance_OnMenu_RightPerformed(object sender, EventArgs e) {
        if(currentWindow != null) {
            currentWindow.OnRightInput();
        }
    }

    private void Instance_OnMenu_LeftPerformed(object sender, EventArgs e) {
        if (currentWindow != null) {
            currentWindow.OnLeftInput();
        }
    }

    private void Instance_OnMenu_DownPerformed(object sender, EventArgs e) {
        if (currentWindow != null) {
            currentWindow.OnDownInput();
        }
    }

    private void Instance_OnMenu_UpPerformed(object sender, EventArgs e) {
        if (currentWindow != null) {
            currentWindow.OnUpInput();
        }
    }

    private void Instance_OnMenu_CancelPerformed(object sender, EventArgs e) {
        if (currentWindow != null) {
            currentWindow.OnCancelInput();
        }
    }

    private void Instance_OnMenu_AcceptPerformed(object sender, EventArgs e) {
        if (currentWindow != null) {
            currentWindow.OnAcceptInput();
        }
    }
}
