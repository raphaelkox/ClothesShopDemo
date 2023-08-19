using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItemUI : MonoBehaviour
{
    [SerializeField] protected ItemSO item;
    [SerializeField] protected Image iconObject;

    public ItemSO Getitem() {
        return item;
    }

    public virtual void SetItem(ItemSO item) {
        this.item = item;
        iconObject.sprite = item.Icon;
    }

    public void Reset() {
        gameObject.SetActive(false);
        transform.SetAsLastSibling();
        item = null;
    }
}
