using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
