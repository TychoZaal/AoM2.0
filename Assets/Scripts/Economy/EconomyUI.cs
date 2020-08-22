using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EconomyUI : MonoBehaviour
{
    [System.Serializable]
    public struct ResourceUIElement
    {
        public string name;
        public Text currentAmount;
        public Text workers;

        public ResourceUIElement(string name, Text currentAmount, Text workers)
        {
            this.name = name;
            this.currentAmount = currentAmount;
            this.workers = workers;
        }
    }

    [SerializeField]
    private Economy economy;

    public List<ResourceUIElement> economyUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResource("Food");
        UpdateResource("Wood");
        UpdateResource("Population");
        UpdateResource("Gold");
        UpdateResource("Faith");
    }

    private void UpdateResource(string resourceName)
    {
        Economy.Resource resource = economy.GetResource(resourceName);
        ResourceUIElement resourceUI = GetResourceUI(resourceName);

        string slash = "/";

        string maxAmount;

        if (resource.maxAmount >= 135)
        {
            maxAmount = "";
        }
        else
        {
            maxAmount = slash + resource.maxAmount.ToString();
        }

        resourceUI.currentAmount.text = resource.currentAmount.ToString() + maxAmount;

        if (resource.workers < 1000)
        {
            resourceUI.workers.text = slash + resource.workers.ToString();
        }
        else
        {
            resourceUI.workers.text = "";
        }
    }

    public ResourceUIElement GetResourceUI(string name)
    {
        for (int i = 0; i < economyUI.Count; i++)
        {
            if (economyUI[i].name == name)
                return economyUI[i];
        }
        return new ResourceUIElement(null, null, null);
    }
}
