using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// The Equipment Contents and Interface Handler
// - Works similar to the Inventory Panel, but don't need Constant Checking
public class EquipmentPanel: MonoBehaviour {
    // Right-click Event Listener for Equipment SLots Event Signaler
    public event Action<Item> OnEquipmentRightClickedEvent;
    
    [SerializeField] Transform slotsContainer;
    // The Item Slots representing the Inventory UI
    [SerializeField] EquipmentSlot[] slots;

     void Start() {
        // Link Listener to each SLots Indivicudual Event
        for (int i = 0; i < slots.Length; i++) {
            slots[i].OnRightClickEvent += OnEquipmentRightClickedEvent;
        }
    }

    void OnValidate() {
        // Auto Assign SLots
        if (slotsContainer != null) {
            slots = slotsContainer.gameObject.GetComponentsInChildren<EquipmentSlot>();
        }
    }

    // Setting the Slot Equipment Item: Automatically Refresh the Icons and Appearance on Adding / Removing Items
    //  - Outing the Previously Equipped (even when null)
    public bool AddEquipment(Equipment equipment, out Equipment previousEquipment) {
        // Iterate through the Slots to Find the Matching Type
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].type == equipment.type) {
                // REturn the Previously Equipped for Later Operations
                previousEquipment = (Equipment)slots[i].Item;
                //  Assign the Equipment to the Slot Item
                slots[i].Item = equipment;
                return true;
            }
        }
        // Not Equippable, and No Equipment Previously
        previousEquipment = null;
        return false;
    }

    // Removing with the same Mechanics
    public bool RemoveEquipment(Equipment equipment) {
        // Check if the Item is currently Equipping in any SLots
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].Item == equipment) {
                // Set the Currently Equiped Slot's Item to null (the Item would be Added back to the Inventory later with Higher Level Inventory Manager)
                slots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
