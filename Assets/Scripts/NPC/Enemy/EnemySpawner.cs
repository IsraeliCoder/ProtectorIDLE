using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int id;

    private PathManagment pathManagment;

    public GameObject[] EnemyToSpawn;
    public float[] SpawnInterval;
    public float[] RunningTimes;

    public EnemyStorage enemyStorage;
    public StatsManager statsManager;


    public void Start()
    {
        PropsCreator propCreator = FindObjectOfType<PropsCreator>();
        EnemyToSpawn = new GameObject[propCreator.Enemies.Length];
        for (int i = 0; i < propCreator.Enemies.Length; i++)
            EnemyToSpawn[i] = propCreator.Enemies[i];


        SpawnInterval = new float[EnemyToSpawn.Length];
        SpawnInterval[0] = enemyStorage.EnemyStartingSpwaningTime;

        RunningTimes = new float[1];
        RunningTimes[0] = enemyStorage.EnemyStartingSpwaningTime;
        pathManagment = new PathManagment();
    }

    void FixedUpdate()
    {

        for (int i = 0; i < RunningTimes.Length; i++)
        {

            RunningTimes[i] += Time.fixedDeltaTime;
            if (RunningTimes[i] >= SpawnInterval[i])
            {

                GameObject enemy = Instantiate(EnemyToSpawn[i]);
                enemy.transform.position = transform.position + new Vector3(0, 1, 0);

                EnemyMovement enemyMovement = enemy.AddComponent<EnemyMovement>();
                enemyMovement.pathController = pathManagment.GeneratePathController(GameManager.CurrentBluePrint);
                enemyMovement.statsManager = statsManager;
                enemyMovement.enemyStorage = enemyStorage;

                EnemyParam param = enemy.AddComponent<EnemyParam>();
                param.enemyStorage = enemyStorage;
                param.SetData(enemyStorage, statsManager);

                RunningTimes[i] = 0;

            }

        }

    }
}
