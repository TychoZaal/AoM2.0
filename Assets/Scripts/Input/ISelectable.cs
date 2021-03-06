﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    void ShowHealthBar(GameObject canvas, bool currentlySelected);

    void AddToSelectablesList();

    void Remove();

    void ShowSelectionRing(GameObject ring, bool currentlySelected);
}
