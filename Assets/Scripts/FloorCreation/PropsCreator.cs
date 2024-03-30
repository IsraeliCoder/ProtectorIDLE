using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCreator : MonoBehaviour
{

    public GameObject[] PropsRef;

    public List<GameObject> AddProps(GameObject[,] backgroundObjects, float propAppearChance=0.3f)
    {

        int sizeX = backgroundObjects.GetLength(0);
        int sizeY = backgroundObjects.GetLength(1);

        List<GameObject> toReturn = new List<GameObject>();

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (backgroundObjects[i,j].name.StartsWith("Tile_Center") || 
                        backgroundObjects[i, j].name.StartsWith("Tile_Edge") || 
                            backgroundObjects[i, j].name.StartsWith("Tile_Corner"))
                {

                    if (Random.Range(0.0f, 1.0f) < propAppearChance)
                    {
                        GameObject obj = Instantiate(PropsRef.GetRandom());
                        obj.transform.position = backgroundObjects[i, j].transform.position;
                        toReturn.Add(obj);
                    }
                }
            }
        }

        return toReturn;
    
    }

}
