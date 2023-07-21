using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{

  public static BuildingManager Instance { get; private set;}
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCamera;
    public event EventHandler<OnActiveTypeBuildingChangeEventArgs> OnActiveTypeBuildingChange;

    public class OnActiveTypeBuildingChangeEventArgs: EventArgs
    { 
      public BuildingTypeSO activeBuildingType;

    }

    private void Awake() {
      Instance =this;
          buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
       
    }
    private void Start() {
        mainCamera = Camera.main;

   
    }
    private void Update() {
       if(Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject()  )
       {
            if(activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition()))
            {   if(ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                {
                    ResourceManager.Instance.SpendAmount(activeBuildingType.constructionResourceCostArray);
                    Instantiate(activeBuildingType.perfabs, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                }
            
            }
           
       }
       
    }
  
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType =buildingType;
        OnActiveTypeBuildingChange?.Invoke(this, new OnActiveTypeBuildingChangeEventArgs { activeBuildingType = activeBuildingType} );
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
      return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType , Vector3 position)
    {
      BoxCollider2D boxCollider2D = buildingType.perfabs.GetComponent<BoxCollider2D>();
      Collider2D[] collider2DArray =Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset,boxCollider2D.size, 0f);


      bool isAreaClear = collider2DArray.Length ==0;
      if(!isAreaClear) return false;
    
       collider2DArray =Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
      foreach(Collider2D collider2D in collider2DArray )
      {// Collider inside the construjction Radius
        BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
        if(buildingTypeHolder != null){
        if(buildingTypeHolder.buildingType == buildingType)
        {
        // There already a building of this typoe witht the construction Radius! 
        return false;
        }}
      }
        float maxConstructionRadius =25f;
         collider2DArray =Physics2D.OverlapCircleAll(position, maxConstructionRadius);
      foreach(Collider2D collider2D in collider2DArray )
      {// Collider inside the construjction Radius
        BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();

        if(buildingTypeHolder!= null)
        {
        // There already a building of this typoe witht the construction Radius! 
        return true;
        }
      }
      return false;
    }
}
