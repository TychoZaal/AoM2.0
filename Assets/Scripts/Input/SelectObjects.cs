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

                if (!BuildingShop._instance.tryingToPlace)
                {
                    scannedObject.ShowHealthBar();

                    if (Input.GetMouseButtonDown(0))
                    {
                        scannedObject.ToggleSelected();
                    }
                }
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
