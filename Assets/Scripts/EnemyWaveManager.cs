using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWaveManager : MonoBehaviour
{

    public event EventHandler OnWaveNumberChange;

    private enum State
    {
        WaitingToSpawnNewWave,
        SpawningWave,
    }
    [SerializeField] private List<Transform> spawnEnemyPositionList;
    private float spawnNextWaveTimer;
    private float spawnRemainingEnemyTimer;
    private int remainingEnemyAmount, wave;
    private Vector3 spawnPoint;
    private State state;
    [SerializeField]private Transform nextSpawnPosition;
    private void Start()
    {
        state = State.WaitingToSpawnNewWave;
        spawnPoint = spawnEnemyPositionList[UnityEngine.Random.Range(0, spawnEnemyPositionList.Count)].position;
        nextSpawnPosition.position= spawnPoint;
        spawnNextWaveTimer = 3f;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNewWave:
                spawnNextWaveTimer -= Time.deltaTime;
                if (spawnNextWaveTimer <= 0f)
                {
                    SpawnEnemy();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemyAmount > 0)
                {
                    spawnRemainingEnemyTimer -= Time.deltaTime;
                    if (spawnRemainingEnemyTimer < 0f)
                    {
                        spawnRemainingEnemyTimer = UnityEngine.Random.Range(0f, 0.2f);
                        Emeny.Create(spawnPoint + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0, 10f));
                        remainingEnemyAmount--;
                        if (remainingEnemyAmount <= 0)
                        {
                            state = State.WaitingToSpawnNewWave;
                            spawnPoint = spawnEnemyPositionList[UnityEngine.Random.Range(0, spawnEnemyPositionList.Count)].position;
                            nextSpawnPosition.position= spawnPoint;
                            spawnNextWaveTimer = 20f;


                        }

                    }
                }
                break;
        }


    }
    private void SpawnEnemy()
    {
        

        remainingEnemyAmount = 5 + 3* wave;
        state = State.SpawningWave;
        wave ++;
        OnWaveNumberChange?.Invoke(this, EventArgs.Empty);
    }
    public int NextWave(){
        return wave;
    }
    public float NextWaveTimer(){
        return spawnNextWaveTimer;
    }

    public Vector3 NextSpawnpoint()
    {
        return spawnPoint;;
    }
}
