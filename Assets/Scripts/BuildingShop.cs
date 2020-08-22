using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;

public class BuildingShop : MonoBehaviour
{
    [System.Serializable]
    public struct Building
    {
        public GameObject holoObject;
        public GameObject building;
        public string key;

        public Building(GameObject holoObject, GameObject building, string key)
        {
            this.holoObject = holoObject;
            this.building = building;
            this.key = key;
        }
    }

    private bool tryingToPlace = false;

    public bool canPlaceBuilding = true;

    [SerializeField]
    private List<Building> buildings;

    [SerializeField]
    private Building buildingToPlace;

    private GameObject hologramToDisplay;

    [SerializeField]
    private Transform BuildingsHolder, ground;

    [SerializeField]
    private float rotationSnapAmount = 45;

    [SerializeField]
    private Color hologramColor, hologramColorError;

    [ColorUsage(true, true)]
    [SerializeField]
    private Color hologramColorHSV, hologramColorErrorHSV;

    private Renderer holoMaterial;

    // Update is called once per frame
    void Update()
    {
        HandleGeneralInput();

        if (tryingToPlace)
        {
            MoveHologram();
            HandlePlacingInput();
        }
    }

    private void MoveHologram()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Show hologram version 
            hologramToDisplay.transform.position = new Vector3(hit.point.x, ground.transform.position.y, hit.point.z);
        }

        if (canPlaceBuilding)
        {
            var block = new MaterialPropertyBlock();
            holoMaterial.material.SetColor("_BaseColor", hologramColor);
            holoMaterial.material.SetColor("_EmissiveColor", hologramColorHSV);
            holoMaterial.SetPropertyBlock(block);
        }
        else
        {
            var block = new MaterialPropertyBlock();
            block.SetColor("_BaseColor", hologramColorError);
            block.SetColor("_EmissiveColor", hologramColorErrorHSV);
            holoMaterial.SetPropertyBlock(block);
        }
    }

    private void HandleGeneralInput()
    {
        if (Input.GetKeyDown(KeyCode.B) && !tryingToPlace)
        {
            BuyBuilding(GetBuildingWithKey("Barracks"));
        }
    }
    
    private void HandlePlacingInput()
    {
        // Rotate using Q and E
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 newRotation = new Vector3(hologramToDisplay.transform.localEulerAngles.x, hologramToDisplay.transform.localEulerAngles.y + rotationSnapAmount, hologramToDisplay.transform.localEulerAngles.z);
            hologramToDisplay.transform.localEulerAngles = newRotation;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 newRotation = new Vector3(hologramToDisplay.transform.localEulerAngles.x, hologramToDisplay.transform.localEulerAngles.y - rotationSnapAmount, hologramToDisplay.transform.localEulerAngles.z);
            hologramToDisplay.transform.localEulerAngles = newRotation;
        }

        // Cancel using right mouse
        if (Input.GetMouseButtonDown(1))
        {
            RemoveHologram();
        }

        // Place using left mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (canPlaceBuilding)
            {
                PlaceBuilding(buildingToPlace);
            }
            else // TODO Play SFX
            {

            }
        }
    }

    public void BuyBuilding(Building building)
    {
        buildingToPlace = building;
        hologramToDisplay = Instantiate(buildingToPlace.holoObject, transform.position, Quaternion.identity, transform);
        hologramToDisplay.GetComponent<Hologram>().shop = this;
        holoMaterial = hologramToDisplay.GetComponent<Renderer>();
        tryingToPlace = true;
    }

    private void PlaceBuilding(Building building)
    {
        Instantiate(buildingToPlace.building, hologramToDisplay.transform.position, hologramToDisplay.transform.rotation, BuildingsHolder);
        RemoveHologram();
    }

    private void RemoveHologram()
    {
        Destroy(hologramToDisplay);
        buildingToPlace = new Building(null, null, null);
        tryingToPlace = false;
    }

    private Building GetBuildingWithKey(string key)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].key == key)
                return buildings[i];
        }

        return new Building(null, null, "Error");
    }
}
