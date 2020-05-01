using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    Helmet,
    Chest,
    Weapon, Shield,
    Arms, Gloves,
    Legs, Boots,
    Accessory
}

// Equipment: Derived Item Class for the Equippable Variant
[CreateAssetMenu(menuName="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType type;
    [Space]
    public int strength; public float strenghPercent;
    public int agility; public float agilityPercent;
    public int intelligence; public float intelligencePercent;
    public int vitality; public float vitalityPercent;
}
