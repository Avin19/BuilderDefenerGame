using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform perfabs;
    public Sprite sprite;
    public bool hasResourceGenerateData;
    public ResourceGenerateData resourceGenerateData;
    public float minConstructionRadius;
    public ResourceCostAmount[] constructionResourceCostArray;
    public int maxhealthAmount;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach(ResourceCostAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#"+resourceAmount.resourceTypeSO.colorHex+">  "+resourceAmount.resourceTypeSO.nameShort + resourceAmount.amount +"</color>";

          
        }
        return str;
    }
}
