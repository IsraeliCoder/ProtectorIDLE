using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnOneEnemy : MonoBehaviour
{

    public GameObject caster;
    public GameObject enemy;
    public EnemyParam enemyParam;
    public float length;
    public float power = 1;
    public bool onEnemy = false;
    public bool onHero = false;
    public float timeToAttack;
    public float timeToAttakRunner = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemyParam = enemy.GetComponent<EnemyParam>();
        timeToAttack -= Time.fixedDeltaTime * 4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timeToAttakRunner += Time.fixedDeltaTime;

        try
        {
            if (timeToAttakRunner >= timeToAttack)
            {
                timeToAttakRunner = 0;
                enemyParam.hp -= power;
            }

            if (onEnemy)
            {
                transform.position = enemy.transform.position + Vector3.up; ;
            }
            else if (onHero)
            {
                transform.position = caster.transform.position + Vector3.up; ;
            }
            else
            {
                transform.position = (enemy.transform.position - caster.transform.position).normalized * length + caster.transform.position + Vector3.up;
                transform.LookAt(enemy.transform);
            }
        }
        catch
        { 
            // the enemy can go into the portal ... so it might be destroyed.
        }
        finally
        {
        }
    }
}
