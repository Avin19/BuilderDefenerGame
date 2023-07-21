using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform perfabs;
    public Sprite sprite;
    public ResourceGenerateData resourceGenerateData;
    public float minConstructionRadius;
    public ResourceCostAmount[] constructionResourceCostArray;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach(ResourceCostAmount resourceAmount in constructionResourceCostArray)
        {
            str += resourceAmount.resourceTypeSO.nameString + ":" + resourceAmount.amount;

          
        }
        return str;
    }
}
