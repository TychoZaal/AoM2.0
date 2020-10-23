using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjects : MonoBehaviour
{
    public static SelectObjects _instance;

    public List<ISelectable> allSelectables;
    [SerializeField]
    private ISelectable lastSelected, lastHovered;

    [SerializeField]
    private GameObject canvas, selectableRing;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Show health bar of hit object 
            if (hit.transform.gameObject.GetComponent<ISelectable>() != null)
            {
                ISelectable scannedObject = hit.transform.gameObject.GetComponent<ISelectable>();

                lastHovered = scannedObject;

                if (!BuildingShop._instance.tryingToPlace)
                {
                    if (Input.GetMouseButtonDown(0)) // User clicked on selectable
                    {
                        Clicked(scannedObject);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) // User clicked on nothing
                {
                    ResetSelectable();
                }
            }

            if (lastHovered != null)
            {
                Hover(lastHovered, true);

                if (lastSelected != null)
                {
                    lastSelected.ShowSelectionRing(selectableRing, true);
                    lastSelected.ShowHealthBar(canvas, true);
                }
            }
        }

        // Handle selected object input
        if (lastSelected != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                lastSelected.Remove();
                ResetSelectable();
            }
        }
    }

    private void Hover(ISelectable selected, bool toDisplay) // Display health bar and other information UI elements
    {
        Debug.LogError(selected);
    }

    private void Clicked(ISelectable selected)
    {
        lastHovered = selected;
        lastSelected = selected;
    }

    private void ResetSelectable()
    {
        if (lastHovered != null)
            lastHovered.ShowHealthBar(canvas, false);

        if (lastSelected != null)
        {
            lastSelected.ShowSelectionRing(selectableRing, false);
            lastSelected.ShowHealthBar(canvas, false);
        }

        lastHovered = null;
        lastSelected = null;
    }

    public void AddToSelectables(ISelectable selectable)
    {
        allSelectables.Add(selectable);
    }
}
