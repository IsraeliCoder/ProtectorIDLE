using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tile
{ 
    eStart,
    eWalk,
    eBlock,
    eBlockNoProp,
    eEnd,
    eHeroAttackOne,
    eHeroBuff,
    eHeroAttackAll,
    eNone

}

public enum PracticalTile
{ 
    eCorner,
    eFrame,
    eStartingLocation,
    eEndLocation,
    eWalking,
    eWalkingCorner,
    eBlock,
}

public class PracticalTileMeta
{
    
    public PracticalTile tileType;
    public Vector3 rotation;

    public PracticalTileMeta(PracticalTile tileType, Vector3 rotation)
    {
        this.tileType = tileType;
        this.rotation = rotation;
    }

    public PracticalTileMeta(PracticalTile tileType, float y = 0) : this(tileType, new Vector3(0, y, 0))
    {
    }

}

public class FloorStorage : MonoBehaviour
{

    // call as a static variable 
    // singleton like implementation

    public static GameObject CornerTile;
    public static GameObject FrameTile;
    public static GameObject StartLocationTile;
    public static GameObject EndLocationTile;
    public static GameObject WalkingTile;
    public static GameObject WalkingCornerTile;
    public static GameObject BlockTile;

    public static Dictionary<PracticalTile, GameObject> TileEnumToTileObject;
    
    // signleton like implementation 
    // only one instance and Start functiction runs only once
    public static FloorStorage FloorStorageRef;

    // Level storage
    public static List<Tile[,]> Levels = new List<Tile[,]>();

    public static Tile[,] FirstLevel = { 
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
        { Tile.eStart, Tile.eWalk,  Tile.eBlockNoProp, Tile.eWalk, Tile.eEnd     },
        { Tile.eBlock, Tile.eWalk,  Tile.eHeroAttackOne, Tile.eWalk, Tile.eBlockNoProp   },
        { Tile.eBlock, Tile.eWalk,  Tile.eWalk,  Tile.eWalk, Tile.eBlock   },
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
    };

    // Get singleton referense
    public FloorStorage GetFloorStorageRef()
    {
        return FloorStorageRef;
    }

    public GameObject CornerTileRef;
    public GameObject FrameTileRef;
    public GameObject StartLocationTileRef;
    public GameObject EndLocationTileRef;
    public GameObject WalkingTileRef;
    public GameObject WalkingCornerTileRef;
    public GameObject BlockTileRef;

    public void Awake()
    {
        
        FloorStorageRef = this;
        CornerTile = CornerTileRef;
        FrameTile = FrameTileRef;
        StartLocationTile = StartLocationTileRef;
        EndLocationTile = EndLocationTileRef;
        WalkingTile = WalkingTileRef;
        WalkingCornerTile = WalkingCornerTileRef;
        BlockTile = BlockTileRef;

        TileEnumToTileObject = new Dictionary<PracticalTile, GameObject>();
        TileEnumToTileObject[PracticalTile.eCorner] = CornerTileRef;
        TileEnumToTileObject[PracticalTile.eEndLocation] = EndLocationTileRef;
        TileEnumToTileObject[PracticalTile.eFrame] = FrameTileRef;
        TileEnumToTileObject[PracticalTile.eStartingLocation] = StartLocationTileRef;
        TileEnumToTileObject[PracticalTile.eWalking] = WalkingTileRef;
        TileEnumToTileObject[PracticalTile.eWalkingCorner] = WalkingCornerTileRef;
        TileEnumToTileObject[PracticalTile.eBlock] = BlockTileRef;

        Levels.Add(FirstLevel);

    }

}
