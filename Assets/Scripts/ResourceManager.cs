using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance {get; private set;}
    public event EventHandler OnResourceAmountChange;
    private Dictionary<ResourceTypeSO,int> resourceAmountDictionary;

    private void Awake() {
        Instance =this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);


        foreach(ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] =0;
        }
       
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        AddResources(resourceTypeList.list[2],2);
        
        }
    }
    private void Testing()
    {
        foreach( ResourceTypeSO resourceTypeSO in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceTypeSO.nameString + " :" + resourceAmountDictionary[resourceTypeSO]);
        }
    }
    public void AddResources(ResourceTypeSO resourceType ,int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        
        OnResourceAmountChange?.Invoke(this,EventArgs.Empty);
        
    }
    public int GetResourceAmount( ResourceTypeSO resourceTypeSO)
    {
        return resourceAmountDictionary[resourceTypeSO];
    }
    public bool CanAfford (ResourceCostAmount[] resourceAmountArray )
    {
        foreach(ResourceCostAmount resourceCostAmount in resourceAmountArray)
        {
            if(GetResourceAmount(resourceCostAmount.resourceTypeSO)>= resourceCostAmount.amount)
            {
                // can Afford This
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    public void SpendAmount(ResourceCostAmount[] resourceCostAmounts )
    {
        foreach(ResourceCostAmount resourceCostAmount in resourceCostAmounts)
        {
            resourceAmountDictionary[resourceCostAmount.resourceTypeSO] -= resourceCostAmount.amount;
        }
    }
}
