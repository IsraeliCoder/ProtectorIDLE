using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject slash;
    public GameObject SpawnedSlash;


    // Start is called before the first frame update
    public void StartHelper(GameObject enemy)
    {
        slash = FindObjectOfType<Effects>().AttackOnceEffects[0];
        SpawnedSlash = Instantiate(slash);
        AttackOnOneEnemy attackOnEnemy = SpawnedSlash.AddComponent<AttackOnOneEnemy>();
        attackOnEnemy.length = 0.7f;
        attackOnEnemy.enemy = enemy;
        attackOnEnemy.caster = gameObject;
        attackOnEnemy.timeToAttack = 0.6f;
        Destroy(SpawnedSlash, 2f);
    }

}
