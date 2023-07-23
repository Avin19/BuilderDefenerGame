using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUi : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private GameObject btnTemplateUI ;
    private GameObject arrowbtn;
    private Dictionary<BuildingTypeSO ,Transform> btnTransformDictionary;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;



    private void Awake() {
        btnTemplateUI.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        arrowbtn = Instantiate(btnTemplateUI, transform);
        arrowbtn.SetActive(true);
        arrowbtn.GetComponent<RectTransform>().anchoredPosition = new Vector2( 130f ,70f);
        arrowbtn.transform.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowbtn.transform.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0f,-30f);
        arrowbtn.GetComponent<Button>().onClick.AddListener(()=> {BuildingManager.Instance.SetActiveBuildingType(null);});
        int index =2 ;
        MouseEnterExitEvent mouseEnterExitEvent = arrowbtn.GetComponent<MouseEnterExitEvent>();
        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => { ToolTipUI.Instance.Hide(); };



        foreach (BuildingTypeSO buildingtype in buildingTypeList.List)
        {
            if(ignoreBuildingTypeList.Contains(buildingtype)) continue;
           GameObject btnTransform = Instantiate(btnTemplateUI , transform);
           btnTransform.SetActive(true);
           float offset = 130f;
           btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(index*offset , 70f);
           index ++;
           btnTransform.transform.Find("image").GetComponent<Image>().sprite = buildingtype.sprite;


           // Button click listener 

           btnTransform.GetComponent<Button>().onClick.AddListener(() => {BuildingManager.Instance.SetActiveBuildingType(buildingtype);});
           mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
            {
                ToolTipUI.Instance.Show(buildingtype.nameString + "\n" + buildingtype.GetConstructionResourceCostString());
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => { ToolTipUI.Instance.Hide(); };


           btnTransformDictionary[buildingtype] = btnTransform.transform; 
            
        }

    }
    private void Start() {
        BuildingManager.Instance.OnActiveTypeBuildingChange += BuildingManager_OnActiveBuildingTypeBuildingChanged;
         AvtiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeBuildingChanged(object sender, BuildingManager.OnActiveTypeBuildingChangeEventArgs e)
    {
         AvtiveBuildingTypeButton();
    }

  
    private void AvtiveBuildingTypeButton()
    {

        arrowbtn.transform.Find("Selected").gameObject.SetActive(false);
        foreach(BuildingTypeSO buildingType in btnTransformDictionary.Keys)
        {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("Selected").gameObject.SetActive(false);
        }
        BuildingTypeSO activebuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(activebuildingType == null)
        {
            arrowbtn.transform.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
        btnTransformDictionary[activebuildingType].Find("Selected").gameObject.SetActive(true);
        }
    }

}
