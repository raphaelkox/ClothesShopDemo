# ClothesShopDemo

This is a small demo of a shop mechanic where you can buy and sell clothes
interacting with an NPC at a shop counter, as well as a UI window stack, basic
inventory and basic character controller.

The shop is composed of a list with available items and a shopping cart.

Once items are put in the cart, it displays the subtotal (with a visual feedback if the
amount is greater than what the player can afford in case of buying)

A window stack system is used to pass control over to windows pushed on top of others
(closing them in sequence) and the inventory allows bought items to be equipped/unequipped.

This work as a base for any shop (the equippable ClothingItem classes extends
base Item classes, making it possible to create new item categories easily).

The Controls are:

WASD - Movement  
E - Start interaction  
Tab - Toggle the Inventory window  
Esc - Close the topmost window displayed.  

The mouse is used to click on items
and buttons on the interface.

