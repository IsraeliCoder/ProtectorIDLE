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

    public void Start()
    {
        EnemyToSpawn = new GameObject[0];
        SpawnInterval = new float[EnemyToSpawn.Length];
        RunningTimes = new float[SpawnInterval.Length];
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

                RunningTimes[i] = 0;

            }

        }

    }
}
