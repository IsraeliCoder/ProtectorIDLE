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

public static class FloorStorage
{

    public static List<Tile[,]> Levels = new List<Tile[,]>(){ FirstLevel };

    public static Tile[,] FirstLevel = { 
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
        { Tile.eStart, Tile.eWalk,  Tile.eBlock, Tile.eWalk, Tile.eEnd     },
        { Tile.eBlock, Tile.eWalk,  Tile.eBlock, Tile.eWalk, Tile.eBlock   },
        { Tile.eBlock, Tile.eWalk,  Tile.eWalk,  Tile.eWalk, Tile.eBlock   },
        { Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock, Tile.eBlock  },
    };

}
