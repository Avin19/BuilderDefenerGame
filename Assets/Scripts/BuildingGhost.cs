using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingGhost : MonoBehaviour
{
  [SerializeField] private GameObject spriteGameObject;
  private ResourceNearbyOverlay resourceNearbyOverlay;

private void Awake() {
    
    resourceNearbyOverlay = transform.Find("ResoureceNearByOverlay").GetComponent<ResourceNearbyOverlay>();
    Hide();
}
private void Start() {
    BuildingManager.Instance.OnActiveTypeBuildingChange += BuildingManager_OnActiveTypeBuildingChange;
}

private void BuildingManager_OnActiveTypeBuildingChange(object sender, BuildingManager.OnActiveTypeBuildingChangeEventArgs e)
{
      if(  e.activeBuildingType == null)
      {
        Hide();
        resourceNearbyOverlay.Hide();
      }
      else{
        Show(e.activeBuildingType.sprite);
        if(e.activeBuildingType.hasResourceGenerateData)
        {
        resourceNearbyOverlay.Show(e.activeBuildingType.resourceGenerateData);
      }else
      {
        resourceNearbyOverlay.Hide();
      }
      }
}




private void Update() {
   transform.position  = UtilsClass.GetMouseWorldPosition();
}
  private void Show(Sprite ghotsSprite)
  {
    
    spriteGameObject.SetActive(true);
    //spriteGameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = ghotsSprite;
    spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghotsSprite;
  }
  private void Hide()
  {
    spriteGameObject.SetActive(false);
  }
}
