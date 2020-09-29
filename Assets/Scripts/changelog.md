
# ANBCDEPTRAI - HO THAI AN Prototypes and Implementations

# [ITEMS & INVENTORY]
- Scriptable Object Items
  - Name
  - Icon
- Scaled CHaracter Panel UI
- Inventory
  - Grid Layout UI: Group with Equipment and Stat Overview (for easy Comparison on Equipping)
  - List of Storing Items
  - Inventory Item Slot: Item and Icon for each Storing Items
  - Adding + Removing
  - Inventory Refreshing + Updating
- Equipment
  - Layout UI with Equipment Panel and SLots (Derived from Item Slot, but exclusive for Equipment Panel)
  - Derived Scriptable Object
  - Scriptable Types (Derived from Item)
  - Adding + Removing
  - Swapping with Inventory
- Inventory Manager
  - Events
    - Equip from Inventory to Equipments by Right-clicking Item Slots inside Inventory Panel
    - Unequip by Right-clicking Equipment Slots

# [THIRD PERSON CAMERA]
- Pivot Parent
- Following Player
- Orbit around Player by Rotating Pivot with Mouse Movements
- Zooming with Mouse Scroll Wheel by Changing Camera Field of View

# [THIRD PERSON CHARACTER CONTROLLER]
- Custom Physics: Freeze Rotations and Manual Gravity
- Rigidbody Movements
- Rotate towards Moving Direction while keeping Current Rotations
- Moving Relative to Camera Coordinates
- Jumping
- Animation States
  - Idle
  - Running
  - Jumping

# [PROJECT]
- Model and Animations for Player
- Environment: Ground and Range Walls
