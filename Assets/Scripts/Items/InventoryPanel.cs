using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// The Universal Inventory Contents and Interface Handler
public class InventoryPanel: MonoBehaviour {
    // Event for Right-clicking Item Slots
    public event Action<Item> OnItemRightClickedkEvent;
    // Items List separates Logic and Data from Items Grid UI inside the Character UI Panel
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsGrid;
    // The Item Slots representing the Inventory UI
    [SerializeField] ItemSlot[] slots;

    void Start() {
        // Link Inventory Event to each SLots
        for (int i = 0; i < slots.Length; i++) {
            slots[i].OnRightClickEvent += OnItemRightClickedkEvent;
        }
    }

    void OnValidate() {
        // Auto Assign SLots
        if (itemsGrid != null) {
            slots = itemsGrid.gameObject.GetComponentsInChildren<ItemSlot>();
        }
        // Refresh even in Editor
        RefreshInventory();
    }

    public bool AddItem(Item item) {
        // WOn't add if Full Capacity
        if (Full()) {
            return false;
        }
        // Add and Refresh SLots
        items.Add(item);
        RefreshInventory();
        return true;
    }

    public bool RemoveItem(Item item) {
        return items.Remove(item);
        // No need to Refresh on Removing Item, only on Adding and Returning Back
    }

    // Full Capacity Check
    public bool Full() {
        return (items.Count >= slots.Length);
    }

    // Set the Items inside Inventory and Display them on UI SLots
    void RefreshInventory() {
        // Set the Inventory so that the Items in Item Slots and Data are Identical
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