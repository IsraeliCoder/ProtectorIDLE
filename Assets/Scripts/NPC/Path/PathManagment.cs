using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManagment
{

    public void getStartTileIndex(Tile[,] bluePrint, out int indexI, out int indexJ)
    {

        int sizeX = bluePrint.GetLength(0);
        int sizeY = bluePrint.GetLength(1);

        for (int i = 0; i < sizeX; i++)
        {

            for (int j = 0; j < sizeY; j++)
            {

                if (bluePrint[i, j] == Tile.eStart)
                {
                    indexI = i;
                    indexJ = j;
                    return;
                }

            }

        }

        indexI = -1;
        indexJ = -1;

    }

    private void CreateShortestPathControllerHelper(Tile[,] bluePrint, ref bool[,] walkedInMap, PathController pathController,
        int i, int j, Tile toFind = Tile.eEnd)
    {

        List<MatriceMeta<Tile>> neighbours = bluePrint.GetAllNeighbours(ref walkedInMap, i, j);

        foreach (MatriceMeta<Tile> temp in neighbours)
        {
            if (temp.data == Tile.eEnd)
            {
                pathController.AddPathNode(FloorCreator.TilesObjects[temp.i + 1, temp.j + 1].transform.position + new Vector3(0, 1, 0));
                return;
            }
            else if (temp.data == Tile.eWalk)
            {
                pathController.AddPathNode(FloorCreator.TilesObjects[temp.i + 1, temp.j + 1].transform.position + new Vector3(0, 1, 0));
                CreateShortestPathControllerHelper(bluePrint, ref walkedInMap, pathController, temp.i, temp.j, toFind);
                return;
            }
        }

    }

    private PathController CreateShortestPathController(Tile[,] bluePrint, int i, int j, Tile toFind=Tile.eEnd)
    {

        //default value is false
        bool[,] walkedInMap = new bool[bluePrint.GetLength(0), bluePrint.GetLength(1)];
        walkedInMap[i, j] = true;

        PathController toReturn = new PathController();
        CreateShortestPathControllerHelper(bluePrint, ref walkedInMap, toReturn, i, j, toFind);

        return toReturn;

    }

    public PathController GeneratePathController(Tile[,] bluePrint)
    {
        
        int indexI, indexJ;
        getStartTileIndex(bluePrint, out indexI, out indexJ);

        return CreateShortestPathController(bluePrint, indexI, indexJ, Tile.eEnd);

    }

}
