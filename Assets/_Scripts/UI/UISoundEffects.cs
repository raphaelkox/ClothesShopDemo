using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundEffects : MonoBehaviour
{
    public static UISoundEffects Instance;

    [SerializeField] private AudioSource buySellSfx;
    [SerializeField] private AudioSource windowSfx;
    [SerializeField] private AudioSource equipItemSfx;
    [SerializeField] private AudioSource clickSfx;

    private void Awake() {
        Instance = this;
    }

    public void PlayBuySellSfx() {
        buySellSfx.Play();
    }

    public void PlayWindowSfx() {
        windowSfx.Play();
    }

    public void PlayEquipItemSfx() {  
        equipItemSfx.Play();
    }

    public void PlayClickSfx() {
        clickSfx.Play();
    }
}
