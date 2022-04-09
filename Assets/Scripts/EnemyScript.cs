using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //This script will control enemy behavior and stats
    //We have the framework in place for the enemy heatlthbar, just make sure that it works for multiple different enemies

    GameObject manager;
    GameObject player;
    private Animator enemyAnim;
    public int enemyHealth;
    public int enemyMaxHealth;
    float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
     manager = GameObject.FindGameObjectWithTag("BattleManager"); //Battle Referee has this tag
     player = GameObject.FindGameObjectWithTag("Player");
     enemyAnim = gameObject.GetComponent<Animator>();
     attackDelay = 1.0f;
     enemyMaxHealth = 100;
     enemyHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(EnemyAction());
    }

    IEnumerator EnemyAction()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        BattleRef battleRef = manager.GetComponent<BattleRef>();
        if(battleRef.currentState == 1)
        {
            battleRef.currentState = 0;
            yield return new WaitForSeconds(attackDelay);
            enemyAnim.SetTrigger("Attack");
            playerController.PlayerHurt(10);
            yield return new WaitForSeconds(attackDelay);
            playerController.playerCanAttack = true;

        }
        yield return null;
    }
    public void EnemyHurt(int damage)
    {
        enemyAnim.SetTrigger("Hurt");
        enemyHealth -= damage;
    }
}
