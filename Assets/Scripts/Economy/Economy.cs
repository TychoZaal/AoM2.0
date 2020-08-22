using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public static Economy _instance;

    [System.Serializable]
    public struct Resource
    {
        public string name;
        public int currentAmount;
        public int maxAmount;
        public int workers;

        public Resource(string name, int currentAmount, int maxAmount, int workers)
        {
            this.name = name;
            this.currentAmount = currentAmount;
            this.maxAmount = maxAmount;
            this.workers = workers;
        }
    }

    public List<Resource> economy;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public Resource GetResource(string name)
    {
        for (int i = 0; i < economy.Count; i++)
        {
            if (economy[i].name == name)
                return economy[i];
        }
        return new Resource(null, 0, 0, 0);
    }

    public void AdjustResourceBalance(Cost cost)
    {
        for (int i = 0; i < economy.Count; i++)
        {
            switch(economy[i].name)
            {
                case "Food":
                    // Add addition
                    economy[i] = new Resource(economy[i].name, economy[i].currentAmount + cost.foodCost, economy[i].maxAmount, economy[i].workers);
                    break;
                case "Wood":
                    // Add addition
                    economy[i] = new Resource(economy[i].name, economy[i].currentAmount + cost.woodCost, economy[i].maxAmount, economy[i].workers);
                    break;
                case "Gold":
                    // Add addition
                    economy[i] = new Resource(economy[i].name, economy[i].currentAmount + cost.goldCost, economy[i].maxAmount, economy[i].workers);
                    break;
                case "Faith":
                    // Add addition
                    economy[i] = new Resource(economy[i].name, economy[i].currentAmount + cost.faithCost, economy[i].maxAmount, economy[i].workers);
                    break;
            }
        }
    }

    public bool CanAfford(int foodCost, int woodCost, int goldCost, int faithCost)
    {
        if (GetResource("Food").currentAmount > foodCost)
        {
            if (GetResource("Wood").currentAmount > foodCost)
            {
                if (GetResource("Gold").currentAmount > foodCost)
                {
                    if (GetResource("Faith").currentAmount > foodCost)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
