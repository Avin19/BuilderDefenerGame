using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    private TextMeshProUGUI nextWaveText;
    private TextMeshProUGUI waveTimerText;
    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private RectTransform spwanPointIndicator;
    private RectTransform enemyIncomingIndicator;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        enemyIncomingIndicator = transform.Find("icomingenemyindicator").GetComponent<RectTransform>();
        spwanPointIndicator = transform.Find("spawnpointIndicator").GetComponent<RectTransform>();
        nextWaveText = transform.Find("NextWaveText").GetComponent<TextMeshProUGUI>();
        waveTimerText = transform.Find("NextWaveTimerText").GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        enemyWaveManager.OnWaveNumberChange += EmenyWaveManager_OnWaveNumberChange;
        SetWaveNumberText("Wave " + enemyWaveManager.NextWave());
    }

    private void EmenyWaveManager_OnWaveNumberChange(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.NextWave());

    }

    private void Update()
    {
        NextWaveMessageHandle();
        HandleEnemyClosed();
        HandleNextSpawnPointIndicator();


    }
    private void NextWaveMessageHandle()
    {
        
        float nextWaveTimer = enemyWaveManager.NextWaveTimer();
        if (nextWaveTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveTimer.ToString("F1") + "s");
        }

    }
    private void HandleNextSpawnPointIndicator()
    {
            Vector3 dirToNextPoint = (enemyWaveManager.NextSpawnpoint() - mainCamera.transform.position).normalized;
        spwanPointIndicator.anchoredPosition = dirToNextPoint * 300f;
        spwanPointIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotationAngle(dirToNextPoint));

        float distanceToNextPoint = Vector3.Distance(enemyWaveManager.NextSpawnpoint(), mainCamera.transform.position);
        spwanPointIndicator.gameObject.SetActive(distanceToNextPoint > mainCamera.orthographicSize * 1.5f);
    }
    private void HandleEnemyClosed(){
            
         float maxArea =9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position,maxArea);
        Emeny targetEmeny = null;
        foreach(Collider2D collider2d in collider2DArray)
        {
            Emeny emeny = collider2d.GetComponent<Emeny>();

            if(emeny != null)
            {
                // There is an emeny 
                if(targetEmeny == null)
                {
                    targetEmeny= emeny;
                }
                else{
                    if(Vector3.Distance(transform.position,targetEmeny.transform.position)< Vector3.Distance(transform.position,emeny.transform.position))
                    {
                        targetEmeny= emeny;
                    }
                }
            }
        }

        if(targetEmeny!= null){
            Vector3 directionToNextEnemy = (targetEmeny.transform.position - mainCamera.transform.position).normalized;
        enemyIncomingIndicator.anchoredPosition = directionToNextEnemy * 200f;
        enemyIncomingIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotationAngle(directionToNextEnemy));

        float distanceToNextEnemy= Vector3.Distance(targetEmeny.transform.position, mainCamera.transform.position);
        enemyIncomingIndicator.gameObject.SetActive(distanceToNextEnemy > mainCamera.orthographicSize * 1.5f);
        
        }
        else
        {
            enemyIncomingIndicator.gameObject.SetActive(false);
        }
        
    }
    private void SetMessageText(string message)
    {
        waveTimerText.SetText(message);
    }
    private void SetWaveNumberText(string message)
    {
        nextWaveText.SetText(message);
    }
}
