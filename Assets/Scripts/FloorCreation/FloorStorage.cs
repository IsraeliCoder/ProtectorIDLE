using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tile
{ 
    eStart,
    eWalk,
    eBlock,
    eEnd
}

public class FloorStorage : MonoBehaviour
{

    // call as a static variable 
    // singleton like implementation
    public static GameObject StartTile;
    public static GameObject WalkTile;
    public static GameObject BlockTile;
    public static GameObject EndTile;

    public static Dictionary<Tile, GameObject> TileEnumToTileObject;
    
    // signleton like implementation 
    // only one instance and Start functiction runs only once
    public static FloorStorage FloorStorageRef;

    // Level storage
    public static List<Tile[,]> Levels = new List<Tile[,]>();

    public static Tile[,] FirstLevel = { 
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
        { Tile.eStart, Tile.eWalk,  Tile.eBlock, Tile.eWalk, Tile.eEnd     },
        { Tile.eBlock, Tile.eWalk,  Tile.eBlock, Tile.eWalk, Tile.eBlock   },
        { Tile.eBlock, Tile.eWalk,  Tile.eWalk,  Tile.eWalk, Tile.eBlock   },
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
    };

    // Get singleton referense
    public FloorStorage GetFloorStorageRef()
    {
        return FloorStorageRef;
    }

    public GameObject StartTileRef;
    public GameObject WalkTileRef;
    public GameObject BlockTileRef;
    public GameObject EndTileRef;

    public void Awake()
    {
        
        FloorStorageRef = this;
        StartTile = StartTileRef;
        WalkTile = WalkTileRef;
        BlockTile = BlockTileRef;
        EndTile = EndTileRef;

        TileEnumToTileObject = new Dictionary<Tile, GameObject>();
        TileEnumToTileObject[Tile.eStart] = StartTile;
        TileEnumToTileObject[Tile.eBlock] = BlockTile;
        TileEnumToTileObject[Tile.eWalk] = WalkTile;
        TileEnumToTileObject[Tile.eEnd] = EndTile;

        Levels.Add(FirstLevel);

    }

}
