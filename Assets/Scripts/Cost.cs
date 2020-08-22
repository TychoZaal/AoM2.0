using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost : MonoBehaviour
{
    [Header("Negative for costs, positive for gains")]
    public int foodCost;
    public int woodCost;
    public int goldCost;
    public int faithCost;

    public Cost(int foodCost, int woodCost, int goldCost, int faithCost)
    {
        this.foodCost = foodCost;
        this.woodCost = woodCost;
        this.goldCost = goldCost;
        this.faithCost = faithCost;
    }
}
