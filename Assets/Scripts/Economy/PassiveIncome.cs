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
        Economy._instance.AdjustResourceBalance("Gold", Mathf.FloorToInt(rate));
    }
}
