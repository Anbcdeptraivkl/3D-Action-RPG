using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character Panel High-level Manager to Handling Interactions and Operations between INventory Panels and Equipment Panels
public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventoryPanel inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    // Linking the Equip Method into Right-click Event of Inventory Panel and each Inventory Slots
    void Awake() {
        inventory.OnItemRightClickedkEvent += EquipFromInventory;
        equipmentPanel.OnEquipmentRightClickedEvent += UnequipFromEquipments;
    }

    // Equip Item
    // - Can be From Inventory to Equipment Panel
    public void Equip(Equipment equipment) {
        // Remove from Inventory
        if (inventory.RemoveItem(equipment)) {
            Equipment previousEquipment;
            // Add New Equipment by Swapping
            if (equipmentPanel.AddEquipment(equipment, out previousEquipment)) {
                // REturn Previous Equipment back to Inventory
                inventory.AddItem(previousEquipment);
            } else {
                // REturn the Equipping if not Equippable
                inventory.AddItem(equipment);
            }

        }
    }

    // Check Item Type before Equipping
    void EquipFromInventory(Item item) {
        if (item is Equipment) {
            Equip((Equipment)item);
        }
    }

    // Unequip
    public void Unequip(Equipment equipment) {
        // Remove from Equipment Panel
        if (!inventory.Full() && equipmentPanel.RemoveEquipment(equipment)) {
            // - then Add the Item back to the Inventory (if there are still rooms)
            inventory.AddItem(equipment);
        }
    }

    void UnequipFromEquipments(Item item) {
        if (item is Equipment) {
            Unequip((Equipment)item);
        }
    }
}
