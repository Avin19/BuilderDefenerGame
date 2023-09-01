using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        buildingDemolishBtn = transform.Find("DemolishButton");
        buildingRepairBtn = transform.Find("RepairButton");
        if(buildingDemolishBtn!=null)
        {
                HideDemolishBtn();
        }
        if(buildingRepairBtn!= null)
        {
            HideRepairBtn();
        }
       

        healthSystem.SetMaxHealth(buildingType.maxhealthAmount , true);

        healthSystem.OnDamaged += Building_OnDamage;
        healthSystem.OnDie += Building_OnDie;
        healthSystem.OnHeal += Building_onHeal;
    }

    private void Building_onHeal(object sender, EventArgs e)
    {
        if(healthSystem.IsFullHealth())
        {
            HideRepairBtn();
        }
    }

    private void Building_OnDamage(object sender,  EventArgs e)
    {
        ShowRepairBtn();
        Soundmanager.Instance.PlaySound(Soundmanager.Sound.BuildingDamaged);
    }
    private void Building_OnDie(object sender, EventArgs e)
    {
                Soundmanager.Instance.PlaySound(Soundmanager.Sound.BuildingDestoryed);

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
    private void ShowRepairBtn()
    {
        if(buildingRepairBtn!= null)
        {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }
    private void HideRepairBtn()
    {
        if(buildingRepairBtn!=null){
             buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
