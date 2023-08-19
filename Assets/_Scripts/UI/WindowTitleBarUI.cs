using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowTitleBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextObject;
    [SerializeField] private Image titleBar;

    public void SetTitleText(string titleText) {
        titleTextObject.text = titleText;
    }

    public void SetTitleBarColor(Color titleBarColor) {
        titleBar.color = titleBarColor;
    }
}
