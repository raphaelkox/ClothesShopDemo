using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItemUI : MonoBehaviour
{
    [SerializeField] protected ItemData item;
    [SerializeField] protected Image iconObject;

    public ItemData Getitem() {
        return item;
    }

    public virtual void SetItem(ItemData item) {
        this.item = item;
        iconObject.sprite = item.Icon;
    }

    public void Reset() {
        gameObject.SetActive(false);
        transform.SetAsLastSibling();
        item = null;
    }
}
