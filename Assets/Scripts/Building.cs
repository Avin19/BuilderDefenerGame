using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetMaxHealth(buildingType.maxhealthAmount , true);


        healthSystem.OnDie += Building_OnDie;
    }
    private void Building_OnDie(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
