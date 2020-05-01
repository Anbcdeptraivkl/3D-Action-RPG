using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Scriptable Component for All Items
[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    // Base Using Method
}
