using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInteraction
{
    event EventHandler OnSelected;
    event EventHandler OnUnselected;
    event EventHandler OnInteract;
    event EventHandler OnInteractEnd;

    void Select();
    void Unselect();
    void Interact();
    void InteractEnd();
}
