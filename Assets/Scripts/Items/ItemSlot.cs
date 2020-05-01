using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Base Properties and Behaviours of the many types Item SLot inside Character Panels
// - Setting and Swapping Item
// - Detect Input Events (i.e Equipping)
public class ItemSlot: MonoBehaviour, IPointerClickHandler {
    // Right-click Events affecting Item Types: REceiving an Item as Parameter
    public event Action<Item> OnRightClickEvent;
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
            // NUll Check: Turn off Icon on No Item inside Slot
            if (_item == null) {
                icon.enabled = false;
            } else {
                // Setting up Icon Image
                icon.sprite = _item.icon;
                icon.enabled = true;
            }
        }
    }

    // CLicking Events
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check the Mouse Button
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) {
            Debug.Log("Right clicked");
            // Check Slot item
            if (_item != null && OnRightClickEvent != null) {
                OnRightClickEvent(_item);
                Debug.Log("Registering Event");
            }
        }
    }

    // Base Validation for all Item TYpes
    // Get a null Image Component on Starting up
    protected virtual void OnValidate() {
        if (icon == null)
            icon = GetComponent<Image>();
    }
}