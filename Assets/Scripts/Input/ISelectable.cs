using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    void ShowHealthBar();

    void AddToSelectablesList();

    void ToggleSelected();
}
