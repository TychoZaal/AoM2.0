using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;

public class BuildingShop : MonoBehaviour
{
    [System.Serializable]
    public struct BuildingStruct
    {
        public GameObject holoObject;
        public Building building;
        public string key;

        public BuildingStruct(GameObject holoObject, Building building, string key)
        {
            this.holoObject = holoObject;
            this.building = building;
            this.key = key;
        }
    }

    private bool tryingToPlace = false;

    public bool canPlaceBuilding = true;

    [SerializeField]
    private List<BuildingStruct> buildings;

    [SerializeField]
    private BuildingStruct buildingToPlace;

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
        if (!tryingToPlace) 
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BuyBuilding(GetBuildingWithKey("Barracks"));
            }
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
                Debug.LogError("Invalid placement");
            }
        }
    }

    public void BuyBuilding(BuildingStruct building)
    {
        if (CanBuyBuilding(building.key))
        {
            buildingToPlace = building;
            hologramToDisplay = Instantiate(buildingToPlace.holoObject, transform.position, Quaternion.identity, transform);
            hologramToDisplay.GetComponent<Hologram>().shop = this;
            holoMaterial = hologramToDisplay.GetComponent<Renderer>();
            tryingToPlace = true;
        }
        else
        {
            // TODO play SFX 
            Debug.LogError("Not enough resources");
        }
    }

    private void PlaceBuilding(BuildingStruct building)
    {
        Instantiate(buildingToPlace.building, hologramToDisplay.transform.position, hologramToDisplay.transform.rotation, BuildingsHolder);
        Economy._instance.AdjustResourceBalance(buildingToPlace.building.cost);
        RemoveHologram();
    }

    private void RemoveHologram()
    {
        Destroy(hologramToDisplay);
        buildingToPlace = new BuildingStruct(null, null, null);
        tryingToPlace = false;
    }

    private BuildingStruct GetBuildingWithKey(string key)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].key == key)
                return buildings[i];
        }

        return new BuildingStruct(null, null, "Error");
    }

    private bool CanBuyBuilding(string key)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].key == key)
            {
                return Economy._instance.CanAfford(buildings[i].building.cost.foodCost, buildings[i].building.cost.woodCost, buildings[i].building.cost.goldCost, buildings[i].building.cost.faithCost);
            }
        }

        return false;
    }
}
