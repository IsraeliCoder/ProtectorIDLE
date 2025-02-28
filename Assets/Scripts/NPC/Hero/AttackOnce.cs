using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnce : MonoBehaviour
{

    public float radius = 4f;

    public float runningTimeForAttack = 0;
    public float TimeForAttack = 3;
    public float runningTimeForFinding = 0;
    public float TimeForFinding = 0.5f;

    public GameObject EnemyToAttack = null;
    public GameObject ExitTile = null;
    public Animator animator;

    public void OnMouseUp()
    {
        runningTimeForAttack = TimeForAttack;
    }

    private void Start()
    {
        ExitTile = FindObjectOfType<ExitBlock>().gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        runningTimeForAttack += Time.fixedDeltaTime;
        runningTimeForFinding += Time.fixedDeltaTime;

        if (runningTimeForFinding >= TimeForFinding)
        {
            EnemyMovement target = null;
            float smallestDistance = 1000;
            EnemyMovement[] objects = FindObjectsOfType<EnemyMovement>();

            foreach (EnemyMovement enemy in objects)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < radius)
                {
                    if (Vector3.Distance(enemy.transform.position, ExitTile.transform.position) < smallestDistance)
                    {
                        target = enemy;
                    }
                }
            }

            EnemyToAttack = target == null ? null : target.gameObject;
            animator.SetBool("Attack", false);
            runningTimeForFinding = 0;
        }

        if (runningTimeForAttack >= TimeForAttack)
        {
            if (EnemyToAttack != null)
            {
                animator.SetBool("Attack",true);
                AttackEffect tempAtk = gameObject.AddComponent<AttackEffect>();
                tempAtk.StartHelper(EnemyToAttack);
                EnemyToAttack = null;
            }
            runningTimeForAttack = 0;
        }

        if (EnemyToAttack != null)
        {
            transform.LookAt(EnemyToAttack.transform);
        }

    }
}
