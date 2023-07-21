using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

  public static int GetNearByResourceAmount(ResourceGenerateData resourceGenerateData ,Vector3 position)
  {
      Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position ,resourceGenerateData.resourceDetectionRadius);
       int nearByResopurce =0;
      foreach(Collider2D collider2d in collider2DArray)
      {
        ResourceNode resourceNode = collider2d.GetComponent<ResourceNode>();
        if( resourceNode != null)
        {
          if(resourceNode.resourceTypeSO == resourceGenerateData.resourceType)
          { //Same type
          nearByResopurce++;
          }
          // It is a resourece Node 
        }
      }
      nearByResopurce = Mathf.Clamp(nearByResopurce , 0 , resourceGenerateData.mxResourceAmount);
      return nearByResopurce; 
  }
  private float timer;
  private float maxTimer;
  private BuildingTypeSO buildingType;
  private ResourceGenerateData resourceGenerateData;

    private void Awake() {
       resourceGenerateData = GetComponent<BuildingTypeHolder>().buildingType.resourceGenerateData;
        maxTimer = resourceGenerateData.maxTimer;
    }
    private void Start() {
      
      int nearByResopurce = GetNearByResourceAmount(resourceGenerateData,transform.position); 
      if(nearByResopurce == 0)
      {
        // No neaarby resource node
        //Disable resource generator

        enabled =false;

      }
      else{
        maxTimer = (resourceGenerateData.maxTimer/2f)+resourceGenerateData.maxTimer*(1- (float)nearByResopurce/resourceGenerateData.mxResourceAmount);
      }
      //Debug.Log("Nearby Resource : " + nearByResopurce);
    }
  private void Update() {
    timer -= Time.deltaTime;
   
    if(timer <= 0f)
    {
        timer += maxTimer;
        //Debug.Log(buildingType.resourceGenerateData.resourceType.nameString);
        ResourceManager.Instance.AddResources(resourceGenerateData.resourceType ,1);

    }
    
  }
  public ResourceGenerateData GetResourceGenerateData()
  {
    return resourceGenerateData;
  }

  public float GetTimerNormalized()
  {
    return timer/maxTimer;
  }
  public float Get5AmountGeneratorPerSecond()
  {
    return 1/ maxTimer;
  }
}
