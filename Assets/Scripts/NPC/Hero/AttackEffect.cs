using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject slash;
    public GameObject SpawnedSlash;
    public GameObject Enemy;
    public HeroStorage heroStorage;


    // Start is called before the first frame update
    public void SetEnemy(GameObject enemy, HeroStorage heroStorage)
    {
        Enemy = enemy;
        this.heroStorage = heroStorage;
    }

    public void Start()
    {

        slash = FindObjectOfType<Effects>().AttackOnceEffects[0];
        SpawnedSlash = Instantiate(slash);
        AttackOnOneEnemy attackOnEnemy = SpawnedSlash.AddComponent<AttackOnOneEnemy>();
        attackOnEnemy.length = heroStorage.lengthFromCaster;
        attackOnEnemy.enemy = Enemy;
        attackOnEnemy.caster = gameObject;
        attackOnEnemy.timeToAttack = heroStorage.timeToInflictDamage;
        attackOnEnemy.power = heroStorage.power;
        Destroy(SpawnedSlash, heroStorage.timeToDestroy);
    }

}
