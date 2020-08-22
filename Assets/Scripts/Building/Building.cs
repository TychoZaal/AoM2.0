using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    [SerializeField]
    public Cost cost;

    [SerializeField]
    protected int currentHealth, maxHealth;

    public virtual void DestroyBuilding()
    {
        Destroy(gameObject);
    }
}
