using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding, ISelectable
{
    [SerializeField]
    public Cost cost;

    [SerializeField]
    protected int currentHealth, maxHealth;

    public void AddToSelectablesList()
    {
        throw new System.NotImplementedException();
    }

    public virtual void DestroyBuilding()
    {
        Destroy(gameObject);
    }

    public void ShowHealthBar()
    {
        Debug.LogError(currentHealth);
    }

    public void ToggleSelected()
    {
        SelectObjects._instance.SetSelectables(gameObject);
    }
}
