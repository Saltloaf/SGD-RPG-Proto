using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRef : MonoBehaviour
{
    //This script will be for managing turn order maybe?

    //currentState: 0 - player's turn, 1 - enemy turn, 2 - game over
    public int currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = 0;
        // I tried messing with enumerations instead of integers, but I didn't find a way to use them well
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
