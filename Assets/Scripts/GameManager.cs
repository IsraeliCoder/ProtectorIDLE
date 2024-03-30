using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Level = 0;
    public FloorCreator FloorCreatorRef;

    // Start is called before the first frame update
    void Start()
    {
        Tile[,] levelGround = FloorStorage.Levels[Level];
        FloorCreatorRef.CreateNew(levelGround);
        FloorCreatorRef.GenerateProps(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
