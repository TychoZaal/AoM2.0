using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    void ShowHealthBar(GameObject canvas, bool currentlySelected);

    void AddToSelectablesList();

    void ToggleSelected();

    void Remove();
}
