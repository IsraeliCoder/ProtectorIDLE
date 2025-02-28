using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParam : MonoBehaviour
{

    public float hp=1;
    public bool killed = false;


    public void FixedUpdate()
    {
        if (!killed && hp <= 0)
        {
            killed = true;
            Destroy(gameObject);
        }
    }

}
