using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ItemSO : ScriptableObject
{
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public float Price;

    public virtual bool Use() { return false; }
}
