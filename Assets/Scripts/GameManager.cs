using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This script will be for managing overall game states

    GameObject[] screenElements;
    GameObject manager;
    GameObject gameOver;
    bool i; //arbitrary boolean
    
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("BattleManager");
        screenElements = GameObject.FindGameObjectsWithTag("UI");
        gameOver = GameObject.Find("Game Over Text");

        gameOver.GetComponent<Text>().enabled = false; //Hide game over text at start
        i = true;
    }

    void Update()
    {
        BattleRef battleRef = manager.GetComponent<BattleRef>();
        if (battleRef.currentState == 2 && i) // When the player dies
        {
            i = false; //prevent object null reference by running once. There's probably a better way of doing it idk
            foreach(GameObject screenElement in screenElements)
            {
                screenElement.SetActive(false); //certain UI elements with the tag "UI" disappear upon Game Over
            }
            gameOver = GameObject.Find("Game Over Text"); 
            gameOver.GetComponent<Text>().enabled = true; //Display "GAME OVER"
        }
    }
}
