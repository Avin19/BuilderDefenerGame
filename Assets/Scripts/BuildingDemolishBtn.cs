using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
  private Button demolisgBtn;
  [SerializeField] private Building building;
  private void Awake() {
    transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=>{
    BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
    foreach( ResourceCostAmount resourceCostAmount in buildingType.constructionResourceCostArray)
    {
        ResourceManager.Instance.AddResources(resourceCostAmount.resourceTypeSO , Mathf.FloorToInt(resourceCostAmount.amount*0.6f));
    }
    Destroy(building.gameObject);
    
    });
  }


}
