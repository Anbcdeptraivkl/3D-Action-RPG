using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Universal Inventory Manager and Interface Handler
public class Inventory: MonoBehaviour {
    // Items List separates Logic and Data from Items Grid UI inside the Character UI Panel
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsGrid;
    // The Item Slots representing the Inventory UI
    [SerializeField] ItemSlot[] slots;

    void OnValidate() {
        // Auto Assign SLots
        if (itemsGrid != null) {
            slots = itemsGrid.gameObject.GetComponentsInChildren<ItemSlot>();
        }
        // Refresh even in Editor
        RefreshInventory();
    }

    void RefreshInventory() {
        // Set and Check the Inventory so that the Items in Item Slots and Data are Identical
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++) {
            slots[i].Item = items[i];
        }
        // The Remaining Item Slots are left blank without items
        for (; i < slots.Length; i++) {
            slots[i].Item = null;
        }
    }
}