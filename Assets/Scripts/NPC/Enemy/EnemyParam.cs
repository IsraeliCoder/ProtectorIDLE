using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParam : MonoBehaviour
{

    public float hp;
    public bool killed = false;
    public StatsManager statsManager;
    public EnemyStorage enemyStorage;

    public void SetData(EnemyStorage storage, StatsManager statsManager)
    {
        hp = storage.Hp;
        this.statsManager = statsManager;        
    }



    public void FixedUpdate()
    {
        if (!killed && hp <= 0)
        {
            killed = true;
            Destroy(gameObject);
            statsManager.addMoney(new Number(0, enemyStorage.value, 0));
        }
    }

}
