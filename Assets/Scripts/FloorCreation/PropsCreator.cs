using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCreator : MonoBehaviour
{

    public GameObject[] Enemies;

    public GameObject[] PropsRef;

    public GameObject EnemyEnter;
    public GameObject EnemyExit;

    public GameObject HeroAttackOnce;
    public GameObject HeroAttackAll;
    public GameObject HeroBuff;

    public List<GameObject> AddProps(GameObject[,] backgroundObjects, float propAppearChance=0.3f)
    {

        int sizeX = backgroundObjects.GetLength(0);
        int sizeY = backgroundObjects.GetLength(1);

        List<GameObject> toReturn = new List<GameObject>();

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {

                Tile tile = Tile.eNone;
                if (i > 0 && i < sizeX - 1 && j > 0 && j < sizeY - 1)
                    tile = GameManager.CurrentBluePrint[i - 1, j - 1];

                if (tile == Tile.eStart || tile == Tile.eEnd)
                { 
                    GameObject obj = Instantiate(tile == Tile.eStart ? EnemyEnter : EnemyExit);
                    obj.transform.position = backgroundObjects[i, j].transform.position + Vector3.up;

                    if (tile == Tile.eStart)
                    {
                        obj.transform.localScale *= 0.5f;
                    }
                    else
                    {
                        obj.transform.position += Vector3.up;
                    }

                    toReturn.Add(obj);
                }
                else if (tile == Tile.eHeroAttackOne || tile == Tile.eHeroAttackAll || tile == Tile.eHeroBuff)
                {
                    GameObject obj = Instantiate(
                        tile == Tile.eHeroAttackOne ? HeroAttackOnce : 
                            (tile == Tile.eHeroAttackAll? HeroAttackAll : HeroBuff)
                    );

                    if (tile == Tile.eHeroAttackOne)
                    {
                        AttackOnce atkOnce = obj.AddComponent<AttackOnce>();
                        atkOnce.SetAtkMetaData(GetComponent<HeroStorage>());
                    }

                    obj.transform.position = backgroundObjects[i, j].transform.position + Vector3.up;
                    obj.transform.Rotate(new Vector3(0, 90, 0));
                    
                }
                else if (!backgroundObjects[i,j].name.Contains("Road"))
                {

                    if (Random.Range(0.0f, 1.0f) < propAppearChance)
                    {
                        GameObject obj = Instantiate(tile == Tile.eBlockNoProp ? PropsRef[0] : PropsRef.GetRandom());
                        obj.transform.position = backgroundObjects[i, j].transform.position;
                        toReturn.Add(obj);
                    }
                }
            }
        }

        return toReturn;
    
    }

}
