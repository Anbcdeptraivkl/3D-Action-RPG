using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Equipment SLot: Derived Item Slot for the Equipment UI Panels and Functionalities
public class EquipmentSlot : ItemSlot
{
    public EquipmentType type;

    protected override void OnValidate() {
        base.OnValidate();
        // Set Name
        gameObject.name = type.ToString() + " Slot";
    }

    // Implementing Un-equip on Click Event
}
