using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{

    private ResourceGenerateData resourceGenerateData;
    private void Awake() {
        Hide();
    }
    private void Update() {
         int nearByResource = ResourceGenerator.GetNearByResourceAmount(resourceGenerateData,transform.position);
        float percentage = Mathf.RoundToInt((float)nearByResource/ resourceGenerateData.mxResourceAmount *100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percentage + "%");
   
    }
    public void Show(ResourceGenerateData resourceGenerateData)
    {
        this.resourceGenerateData = resourceGenerateData;
        gameObject.SetActive(true);
        transform.Find("image").GetComponent<SpriteRenderer>().sprite = resourceGenerateData.resourceType.sprite;
       
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
