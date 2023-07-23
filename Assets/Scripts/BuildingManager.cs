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
    [SerializeField]private Building hqbuilding;
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
            if(activeBuildingType != null  )
            { 
              if(CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(),out string message))
              {             
                
                   if(ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendAmount(activeBuildingType.constructionResourceCostArray);
                        Instantiate(activeBuildingType.perfabs, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    
                
                else
                {
                  ToolTipUI.Instance.Show(" Can't Afford "+ activeBuildingType.GetConstructionResourceCostString(), new ToolTipUI.ToolTipTimer{timer =2f});
                  
                }
            }
            else
            {
              ToolTipUI.Instance.Show(message, new ToolTipUI.ToolTipTimer{timer =2f});
            }
            }
           
       }
      if(Input.GetKeyDown(KeyCode.T))
      {
        Vector3 movDir = UtilsClass.GetMouseWorldPosition()+UtilsClass.GetRandomDir()*5f;
        Emeny.Create(movDir);

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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType , Vector3 position, out string errorMessage)
    {
      BoxCollider2D boxCollider2D = buildingType.perfabs.GetComponent<BoxCollider2D>();
      Collider2D[] collider2DArray =Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset,boxCollider2D.size, 0f);


      bool isAreaClear = collider2DArray.Length ==0;
      if(!isAreaClear) {
        errorMessage = "Area is Not Clear!";
        return false;
      }

    
       collider2DArray =Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
      foreach(Collider2D collider2D in collider2DArray )
      {// Collider inside the construjction Radius
        BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
        if(buildingTypeHolder != null){
        if(buildingTypeHolder.buildingType == buildingType)
        {
        // There already a building of this typoe witht the construction Radius! 
        errorMessage = "Too close to another building of the same type";
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
        errorMessage = "";
        return true;
        }
      }
      errorMessage = "Too far from any other building!";
      return false;
    }
    public Building GetHqBuilding()
    {
      return hqbuilding;
    }
}
