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
    private GameObject canvas;

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

                Hover(lastHovered, true);

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

                if (lastHovered != null)
                    Hover(lastHovered, true);
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
        selected.ShowHealthBar(canvas, toDisplay);
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

        lastHovered = null;
        lastSelected = null;
    }

    public void AddToSelectables(ISelectable selectable)
    {
        allSelectables.Add(selectable);
    }
}
