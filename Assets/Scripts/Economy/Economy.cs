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

    public void AdjustResourceBalance(string name, int addition)
    {
        for (int i = 0; i < economy.Count; i++)
        {
            if (economy[i].name == name)
            {
                // Add addition
                economy[i] = new Resource(economy[i].name, economy[i].currentAmount + addition, economy[i].maxAmount, economy[i].workers);
            }
        }
    }
}
