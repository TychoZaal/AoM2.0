using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveIncome : MonoBehaviour
{
    [SerializeField]
    private float rate = 1;

    // Update is called once per frame
    void Update()
    {
        Economy._instance.AdjustResourceBalance(new Cost(0, 0, Mathf.FloorToInt(rate), 0));
    }
}
