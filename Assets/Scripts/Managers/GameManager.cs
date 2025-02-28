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

        Number one = new Number(3, 2, 500);
        Number two = new Number(3, 0, 500);

        Number res1 = one.Multiply(one);
        Number res2 = one.Multiply(two);
        Number res3 = two.Multiply(two);

        string res1s = res1.getString();
        string res2s = res2.getString();
        string res3s = res3.getString();
        

        CurrentBluePrint = FloorStorage.Levels[Level];
        FloorCreatorRef.CreateNew(CurrentBluePrint);
        FloorCreatorRef.GenerateProps(0.55f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
