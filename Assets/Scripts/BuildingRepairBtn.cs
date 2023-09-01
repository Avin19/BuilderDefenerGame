using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
  [SerializeField] private HealthSystem healthSystem;
  [SerializeField] private ResourceTypeSO goldResource;
  private void Awake() {
    transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=>{
      int missingHealth = healthSystem.GetHealthMaxInfo()-healthSystem.GetHealthInfo();
      int repairCost = missingHealth/2;
      ResourceCostAmount[] resourceCostAmounts = new ResourceCostAmount [] {new ResourceCostAmount{resourceTypeSO = goldResource , amount = repairCost}};
      if(ResourceManager.Instance.CanAfford(resourceCostAmounts))
      {
            ResourceManager.Instance.SpendAmount(resourceCostAmounts);
              healthSystem.HealFull();

      }
      else
      {
          ToolTipUI.Instance.Show("Can't Afford The Repair Cost" , new ToolTipUI.ToolTipTimer {timer= 2f});
      }
    });
  }


}
