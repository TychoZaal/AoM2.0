using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjects : MonoBehaviour
{
    public static SelectObjects _instance;

    public List<ISelectable> allSelectables;
    public GameObject selectedObject;
    private ISelectable lastSelected;

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

                if (scannedObject != lastSelected && lastSelected != null)
                {
                    lastSelected.ShowHealthBar(canvas, false);
                }

                if (!BuildingShop._instance.tryingToPlace)
                {
                    scannedObject.ShowHealthBar(canvas, true);

                    lastSelected = scannedObject;

                    if (Input.GetMouseButtonDown(0))
                    {
                        scannedObject.ToggleSelected();
                    }
                }
            }
        }

        // Handle selected object input
        if (selectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                selectedObject.GetComponent<ISelectable>().Remove();
                selectedObject = null;
                lastSelected = null;
            }
        }
    }

    public void AddToSelectables(ISelectable selectable)
    {
        allSelectables.Add(selectable);
    }

    public void SetSelectables(GameObject go)
    {
        selectedObject = go;
    }
}
