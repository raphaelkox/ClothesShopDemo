using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData {
    public event EventHandler OnItemEquipped;
    public event EventHandler OnItemUnnequipped;

    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public float Price;

    //extra data for runtime containers
    public bool Blocked;
    public bool Equipped;

    public virtual bool Use() { return false; }

    public void TriggerOnItemEquipped() {
        OnItemEquipped?.Invoke(this, EventArgs.Empty);
    }

    public void TriggerOnItemUnnequipped() {
        OnItemUnnequipped?.Invoke(this, EventArgs.Empty);
    }
}