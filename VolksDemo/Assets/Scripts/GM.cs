using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public Player Niko;
    public Map TestMap;
    // Start is called before the first frame update
    void Start()
    {
        TestMap = new Map();
        TestMap.BuildMap();
        TestMap.ShowMap();

        Niko = new Player();
        Niko.InitPlayer(TestMap.StartTile);
        Niko.ShowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
