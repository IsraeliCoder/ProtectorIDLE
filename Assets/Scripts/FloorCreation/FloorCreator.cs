using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCreator : MonoBehaviour
{

    public GameObject[,] TilesObjects;
    public GameObject[] Props;
    public GameObject[] PropsInitiates;

    public float PropSize;


    public void Awake()
    {
        TilesObjects = null;
        PropsInitiates = null;
    }

    public void CreateNew(Tile[,] groundBluePrint)
    {

        DestroyWorld();
        CreateNewHelper(groundBluePrint);

    }

    private void CreateNewHelper(Tile[,] groundBluePrint)
    {

        int xSize = groundBluePrint.GetLength(0);
        int ySize = groundBluePrint.GetLength(1);
        TilesObjects = new GameObject[xSize, ySize];

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {

                GameObject obj = FloorStorage.TileEnumToTileObject[groundBluePrint[i, j]];
                Vector3 objSize = obj.transform.lossyScale;

                GameObject tempObj = Instantiate(obj);
                tempObj.transform.localScale = obj.transform.localScale * FloorCreationConfig.TileSize;
                tempObj.transform.position = new Vector3(i * objSize.x, objSize.y, j * objSize.z) * 2 * FloorCreationConfig.TileSize;

                TilesObjects[i, j] = tempObj;

            }
        }
    }

    private void DestroyWorld()
    {

        if (TilesObjects != null)
        {

            for (int i = 0; i < TilesObjects.GetLength(0); i++)
            {
                for (int j = 0; j < TilesObjects.GetLength(1); j++)
                {
                    Destroy(TilesObjects[i, j]);
                }
            }

            TilesObjects = null;
        
        }

        if (PropsInitiates != null)
        {

            for (int i = 0; i < PropsInitiates.GetLength(0); i++)
            {
                Destroy(PropsInitiates[i]);
            }

            PropsInitiates = null;
        }

    }

    public void GenerateProps()
    { 
    
    }

}
