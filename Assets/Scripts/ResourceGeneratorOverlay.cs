using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;
    
    private void Start() {
        ResourceGenerateData resourceGenerateData = resourceGenerator.GetResourceGenerateData();
        
        barTransform = transform.Find("bar");
        transform.Find("image").GetComponent<SpriteRenderer>().sprite = resourceGenerateData.resourceType.sprite;

       
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.Get5AmountGeneratorPerSecond().ToString("F1")); 

    }
    private void Update() {
        barTransform.localScale = new Vector3(1- resourceGenerator.GetTimerNormalized(),1f,1f);
    }
}
