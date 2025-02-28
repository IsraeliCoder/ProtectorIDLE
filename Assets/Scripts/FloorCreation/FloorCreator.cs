using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCreator : MonoBehaviour
{

    public static GameObject[,] TilesObjects;
    public List<GameObject> PropsInitiates;
    public PropsCreator PropsCreatorRef;

    public void Awake()
    {
        TilesObjects = null;
        PropsInitiates = null;
    }

    private void locateNeighboursSetIndexes(ref int indexToReturn1, ref int indexToReturn2, int index)
    {
        if (indexToReturn1 == -1)
        {
            indexToReturn1 = index;
        }
        else
        {
            indexToReturn2 = index;
        }
    }

    bool TileIsBlocker(Tile tile)
    {
        return tile == Tile.eBlock || 
            tile == Tile.eBlockNoProp || 
            tile == Tile.eHeroAttackAll || 
            tile == Tile.eHeroAttackOne || 
            tile == Tile.eHeroBuff;
    }
    

    private void locateNeighbours(Tile[,] allGround, int x, int y, out int indexToReturn1, out int indexToReturn2)
    {

        indexToReturn1 = -1;
        indexToReturn2 = -1;

        if (x > 0)
        {
            if (!TileIsBlocker(allGround[x - 1, y]))
            {
                indexToReturn1 = 0;
            }
        }
        if (y < allGround.GetLength(1) - 1)
        {
            if (!TileIsBlocker(allGround[x, y + 1]))
            {
                locateNeighboursSetIndexes(ref indexToReturn1, ref indexToReturn2, 1);
            }
        }
        if (x < allGround.GetLength(0) - 1)
        {
            if (!TileIsBlocker(allGround[x + 1, y]))
            {
                locateNeighboursSetIndexes(ref indexToReturn1, ref indexToReturn2, 2);
            }
        }
        if (y > 0)
        {
            if (!TileIsBlocker(allGround[x, y - 1]))
            {
                locateNeighboursSetIndexes(ref indexToReturn1, ref indexToReturn2, 3);
            }
        }


    }

    private PracticalTileMeta[,] TransformToPracticalTile(Tile[,] groundBluePrint)
    {
        
        int sizeX = groundBluePrint.GetLength(0);
        int sizeY = groundBluePrint.GetLength(1);
        PracticalTileMeta[,] toReturn = new PracticalTileMeta[sizeX + 2, sizeY + 2];

        #region Create Corner
        toReturn[0, 0] = new PracticalTileMeta(PracticalTile.eCorner, 180);
        toReturn[0, sizeY + 1] = new PracticalTileMeta(PracticalTile.eCorner, 270);
        toReturn[sizeX + 1, sizeY + 1] = new PracticalTileMeta(PracticalTile.eCorner, 0);
        toReturn[sizeX + 1, 0] = new PracticalTileMeta(PracticalTile.eCorner, 90);
        #endregion

        #region Create Frame
        for (int i = 0; i < sizeX; i++)
        {
            toReturn[0, i + 1] = new PracticalTileMeta(PracticalTile.eFrame, 270);
            toReturn[sizeX + 1, i + 1] = new PracticalTileMeta(PracticalTile.eFrame, 90);
        }

        for (int i = 0; i < sizeY; i++)
        {
            toReturn[i + 1, 0] = new PracticalTileMeta(PracticalTile.eFrame, 180);
            toReturn[i + 1, sizeY + 1] = new PracticalTileMeta(PracticalTile.eFrame, 0);
        }
        #endregion

        #region Create Map

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                
                Tile tile = groundBluePrint[i, j];
                PracticalTileMeta meta = null;
                
                int walkSpace1;
                int walkSpace2;
                locateNeighbours(groundBluePrint, i, j, out walkSpace1, out walkSpace2);

                #region Get Correct Tile and rotation
                if (tile == Tile.eBlock || 
                    tile == Tile.eBlockNoProp || 
                    tile == Tile.eHeroAttackOne || 
                    tile == Tile.eHeroAttackAll || 
                    tile == Tile.eHeroBuff)
                {
                    meta = new PracticalTileMeta(PracticalTile.eBlock);
                }
                else if (tile == Tile.eStart)
                {
                    meta = new PracticalTileMeta(PracticalTile.eStartingLocation, 90 * ((walkSpace1 + 1) % 4));
                }
                else if (tile == Tile.eEnd)
                {
                    meta = new PracticalTileMeta(PracticalTile.eEndLocation, 90 * ((walkSpace1 + 1) % 4));
                }
                else if (tile == Tile.eWalk)
                {
                    if (i > 0 && groundBluePrint[i - 1, j] == Tile.eWalk && groundBluePrint[i + 1, j] == Tile.eWalk)
                    {
                        meta = new PracticalTileMeta(PracticalTile.eWalking, 90);
                    }
                    else if (j > 0 && groundBluePrint[i, j - 1] == Tile.eWalk && groundBluePrint[i, j + 1] == Tile.eWalk)
                    {
                        meta = new PracticalTileMeta(PracticalTile.eWalking, 0);
                    }
                    else
                    {

                        if (walkSpace2 == 3 && walkSpace1 == 0)
                            walkSpace2 = 0;
                        meta = new PracticalTileMeta(PracticalTile.eWalkingCorner,  ((walkSpace2 + 1) % 4) * 90);
                    }
                }
                #endregion

                toReturn[i + 1, j + 1] = meta;

            }
        }

        #endregion

        return toReturn;

    }

    public void CreateNew(Tile[,] groundBluePrint)
    {

        DestroyWorld();
        PracticalTileMeta[,] bluePrint = TransformToPracticalTile(groundBluePrint);
        CreateNewHelper(bluePrint);

    }

    private void CreateNewHelper(PracticalTileMeta[,] groundBluePrint)
    {

        int xSize = groundBluePrint.GetLength(0);
        int ySize = groundBluePrint.GetLength(1);
        TilesObjects = new GameObject[xSize, ySize];

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {

                GameObject obj = FloorStorage.TileEnumToTileObject[groundBluePrint[i, j].tileType];
                Vector3 objSize = obj.transform.lossyScale;

                GameObject tempObj = Instantiate(obj);
                tempObj.transform.localScale = obj.transform.localScale * FloorCreationConfig.TileSize;
                tempObj.transform.position = new Vector3(i * objSize.x, objSize.y, j * objSize.z) * 2 * FloorCreationConfig.TileSize;

                tempObj.transform.Rotate(groundBluePrint[i, j].rotation);

                TilesObjects[i, j] = tempObj;

                if (groundBluePrint[i, j].tileType == PracticalTile.eStartingLocation)
                {
                    EnemySpawner tempSpawner = tempObj.AddComponent<EnemySpawner>();
                    tempSpawner.id = i * ySize + j;
                }
                else if (groundBluePrint[i, j].tileType == PracticalTile.eEndLocation)
                {
                    ExitBlock tempSpawner = tempObj.AddComponent<ExitBlock>();
                }


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

            for (int i = 0; i < PropsInitiates.Count; i++)
            {
                Destroy(PropsInitiates[i]);
            }

            PropsInitiates = null;

        }

    }

    public void GenerateProps(float propAppearChance)
    {
        PropsInitiates = PropsCreatorRef.AddProps(TilesObjects, propAppearChance);
    }

}
