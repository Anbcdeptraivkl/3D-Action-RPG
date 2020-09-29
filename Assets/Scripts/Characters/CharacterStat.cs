using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
 
// Representing a Character Stat Field (e.g Strength, Intelligence)
//  - Will be Components of Characters: won't be attached to Object so you don't need MonoBehaviour
public class CharacterStat
{
    // The Exposed Calculated Values that will be used In-game
    public float Value {
        get {
            if (changed) {
                changedValue = CalculateModdedValue();
                changed = false;
                return changedValue;
            } else {
                return changedValue;
            }
        }
    }
    // The Base, unmodified Values
    //  - Increased with Levels and Stat Points
    public float baseValue;
    // List of Item Modifiers Affecting this Stat field
    private readonly List<StatModifier> statModifiers;
    // Only Re-calculate when there are some changes happening to the Stat MOdifying process
    bool changed = false;
    float changedValue;
 
    // Default Constructor
    public CharacterStat(float baseValue)
    {
        this.baseValue = baseValue;
        // The Initial Values without any Calculations performed
        changedValue = this.baseValue;
        statModifiers = new List<StatModifier>();
    }

    // Adding & Removing Modifiers
    public void AddModifier(StatModifier mod) {
        changed = true;
        statModifiers.Add(mod);
        // Sort by Order (Lower Order == Higher Priority)
        statModifiers.Sort(CompareModifierOrder);
    }

    public bool RemoveModifier(StatModifier mod) {
        changed = true;
        return statModifiers.Remove(mod);
    }

    public float CalculateModdedValue() {
        float finalValue = baseValue;
        // Applying Modifiers one by one
        for (int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];
            // Deal with each Types of Modifiers differently
            switch (mod.type) {
                case StatModType.Flat: {
                    finalValue += mod.value;
                }
                break;
                
                case StatModType.DirectPercentage: {
                    // Straight-up Applying Percentages to the whole Value
                    finalValue = finalValue * (1 + mod.value);
                }
                break;
            }
        }
        // Return the rounded value (to 1 decimal point)
        //  - Completed with Casting
        return (float)Math.Round(finalValue, 1);
    }

    // Comparison Method for Sorting the Modifiers List by Order
    int CompareModifierOrder(StatModifier a, StatModifier b) {
        if (a.order < b.order) {
            return -1;
        } else {
            if (a.order > b.order) {
                return 1;
            }
        }
        return 0; // Equals
    }
}

// Types of Modifiers
public enum StatModType {
    Flat,
    DirectPercentage // Percentages that apply Directly and Independently
}

public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    // Order of Applying in Calculation Process
    public readonly int order;
 
    public StatModifier(float value, StatModType type, int order)
    {
        this.value = value;
        this.type = type;
        this.order = order;
    }

    //* Overloaded Constructors
    //  - Define Order automatically based on Type Index (from the Enumerated Type)
    public StatModifier(float value, StatModType type): this(value, type, (int)type) {

    }


}