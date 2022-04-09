using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
   
    GameObject player;
    public float healthRatio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        var rectTransform = GetComponent<RectTransform>();

        //Shrinks the health bar
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, (300*playerController.currentHealth)/playerController.maxHealth);
    }
}
