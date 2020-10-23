using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, IBuilding, ISelectable
{
    [SerializeField]
    public Cost cost;

    [SerializeField]
    private float ringRadius;

    [SerializeField]
    protected int currentHealth, maxHealth;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Vector3 canvasOffset, selectableRingOffset;

    public void AddToSelectablesList()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Remove()
    {
        Destroy(gameObject);
    }

    public void ShowHealthBar(GameObject canvas, bool currentlySelected)
    {
        canvas.SetActive(currentlySelected);
        canvas.transform.position = transform.position + canvasOffset;
        healthBar = currentlySelected ? canvas.GetComponent<HealthBar>().healthBar : null;
    }

    protected virtual void Update()
    {
        float healthPercentage = (float)currentHealth / (float)maxHealth;

        if (healthBar != null)
            healthBar.fillAmount = healthPercentage;
    }

    public void ShowSelectionRing(GameObject ring, bool toShow)
    {
        ring.SetActive(toShow);
        ring.transform.position = transform.position + selectableRingOffset;
        ring.transform.localScale = new Vector3(ringRadius, ringRadius, ringRadius);
    }
}
