using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public PathController pathController = null;
    public Vector3 nextNode;
    private bool first = false;
    public StatsManager statsManager;
    public EnemyStorage enemyStorage;



    void FixedUpdate()
    {

        if (pathController != null)
        {

            if (!first)
            {
                nextNode = pathController.GetNextNode();
                transform.LookAt(nextNode);
                first = true;
            }

            transform.position += (nextNode - transform.position).normalized * Time.fixedDeltaTime;

            if ((nextNode - transform.position).magnitude < 0.2)
            {
                if (pathController.EndOfTheRoad())
                {
                    Destroy(gameObject);
                    statsManager.addMoney(new Number(0, enemyStorage.value * -enemyStorage.missedValueMultiplier, 0));
                }
                else
                {
                    nextNode = pathController.GetNextNode();
                    transform.LookAt(nextNode);
                }
            }


        }

    }
}
