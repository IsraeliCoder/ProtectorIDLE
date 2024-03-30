using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Level = 0;
    public FloorCreator FloorCreatorRef;

    public static Tile[,] CurrentBluePrint;

    // Start is called before the first frame update
    void Start()
    {

        CurrentBluePrint = FloorStorage.Levels[Level];
        FloorCreatorRef.CreateNew(CurrentBluePrint);
        FloorCreatorRef.GenerateProps(0.75f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
