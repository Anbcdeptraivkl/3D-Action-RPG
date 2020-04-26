using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Behaviours of the Item SLot inside the Inventory Grid
public class ItemSlot: MonoBehaviour {
    // UI Icon
    [SerializeField] Image icon;
    // The Storing Item
    Item _item;
    public Item Item{
        get {
            return _item;
        }
        set {
            _item = value;
            // NUll Check
            if (_item == null) {
                icon.enabled = false;
            } else {
                // Setting up Icon Image
                icon.sprite = _item.icon;
                icon.enabled = true;
            }
        }
    }

    // Get a null Image Component on Starting up
    void OnValidate() {
        if (icon == null)
            icon = GetComponent<Image>();
    }
}