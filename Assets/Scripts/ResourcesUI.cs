using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResourcesUI : MonoBehaviour
{   

    [SerializeField] private Transform resourceTemplet;
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictonary;
    private void Awake() {
         resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
         resourceTypeTransformDictonary = new Dictionary<ResourceTypeSO, Transform>();
        resourceTemplet.gameObject.SetActive(false);
        int index =0 ; 
        foreach (ResourceTypeSO resourceTypeSO in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplet , transform);
            resourceTransform.gameObject.SetActive(true);

            float offset = -150f;

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset*index , 0f);

            index ++ ;
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceTypeSO.sprite;
            resourceTypeTransformDictonary[resourceTypeSO] = resourceTransform;
        }
    }
    private void Start() {
        ResourceManager.Instance.OnResourceAmountChange +=ResourceManager_OnResourceAmountChanged;
    
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
       UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
         foreach (ResourceTypeSO resourceTypeSO in resourceTypeList.list)
         {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceTypeSO);
            Transform resourceTransform = resourceTypeTransformDictonary[resourceTypeSO];
           
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
         }
    }
}
