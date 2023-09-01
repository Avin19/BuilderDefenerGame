using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{

    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO bulidingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("BuildingConstruction");
        Transform bcTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction= bcTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetUp(bulidingType);
        return buildingConstruction;
    }
    private float construstionTimer;
    private float construstionTimerMax;
    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;


    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = transform.Find("Sprite").GetComponent<SpriteRenderer>().material;
    }
    private void Update() {
        construstionTimer  -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress" ,GetConstrustionTimerNormalized());
        if(construstionTimer<=0f)
        {
            Instantiate(buildingType.perfabs,transform.position, Quaternion.identity);
            Soundmanager.Instance.PlaySound(Soundmanager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }
    private void SetUp(BuildingTypeSO  buildingType)
    {
        this.buildingType = buildingType;

        construstionTimerMax = buildingType.constructionTimerMax;
        construstionTimer= construstionTimerMax;

       spriteRenderer.sprite = buildingType.sprite;
        boxCollider2D.offset = buildingType.perfabs.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.perfabs.GetComponent<BoxCollider2D>().size;
        buildingTypeHolder.buildingType = buildingType;


    }
    public float GetConstrustionTimerNormalized()
    {
        return 1-construstionTimer/construstionTimerMax;
    }
}
