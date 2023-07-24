using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        buildingDemolishBtn = transform.Find("DemolishButton");
        if(buildingDemolishBtn!=null)
        {
                HideDemolishBtn();
        }
       

        healthSystem.SetMaxHealth(buildingType.maxhealthAmount , true);


        healthSystem.OnDie += Building_OnDie;
    }
    private void Building_OnDie(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter() {
        ShowDemolishBtn();
    }
    private void OnMouseExit() {
       HideDemolishBtn();
    }
    private void ShowDemolishBtn()
    {
        if(buildingDemolishBtn!= null)
        {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }
    private void HideDemolishBtn()
    {
        if(buildingDemolishBtn!=null){
             buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}
