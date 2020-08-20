using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), (typeof(Collider)))]
public class Hologram : MonoBehaviour
{
    public BuildingShop shop;

    private void OnCollisionEnter(Collision collision)
    {
        shop.canPlaceBuilding = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        shop.canPlaceBuilding = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        shop.canPlaceBuilding = false;
    }
}
