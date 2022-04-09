using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // This script will be for controlling player animations, movement, and battle stats
    // Also governs actions on player's turn and user inputs

    private Animator playerAnim;
    public int currentHealth;
    public int maxHealth;
    private int indicatorRotationSpeed;
    public int attackStrength;
    GameObject manager;
    GameObject enemy;
    GameObject meleeUI;
    [SerializeField] GameObject turnIndicator;
    Button meleeButton;
    Text meleeText;
    bool hasDied;
    public bool playerCanAttack; //Difference between playerCanAttack and currentState = 0 is that the former's delayed
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("BattleManager"); //Battle Referee has this tag
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        meleeUI = GameObject.Find("Melee Attack Button");
        meleeButton = meleeUI.GetComponent<Button>();
        meleeText = meleeUI.GetComponentInChildren<Text>();
        BattleRef battleRef = manager.GetComponent<BattleRef>();
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        playerAnim = gameObject.GetComponent<Animator>();
        maxHealth = 100; //using this value as a placeholder
        currentHealth = 100;// Probably will implement a new system for starting health
        indicatorRotationSpeed = 180;
        attackStrength = 1; //Increase later
        hasDied = false;
        playerCanAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(PlayerInteractions());
    }

    IEnumerator PlayerInteractions()
    {  
        BattleRef battleRef = manager.GetComponent<BattleRef>(); //figure out a way other than repeatedly copying this

        if (currentHealth <= 0 && hasDied == false)
        {
            hasDied = true;
            playerAnim.SetTrigger("Death");
            battleRef.currentState = 2;
        }
        if (battleRef.currentState == 0 && playerCanAttack)
        {
            meleeButton.interactable = true;
            meleeText.text = "Attack";

            turnIndicator.SetActive(true);
            turnIndicator.transform.Rotate (new Vector3(0, indicatorRotationSpeed, 0) * Time.deltaTime, Space.World);
            // We will probably have a better-looking sprite or smthn playing an animation for the turn indicator
            // Or we'll remove it entirely, since it's not technically needed
        }
        else
        {
            meleeButton.interactable = false;
            turnIndicator.SetActive(false);
            if (playerCanAttack == false) //Has player buttons display "waiting": just for testing probably unnecessary since they're grayed out anyway
            {
                meleeText.text = "Waiting...";
            }
        }
        yield return null;
    }
    public void PlayerHurt(int damage)
    {
        playerAnim.SetTrigger("Hurt");
        currentHealth -= damage;
    }
    public void meleeAttack()
    {
        BattleRef battleRef = manager.GetComponent<BattleRef>();
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        battleRef.currentState = 1;
        playerCanAttack = false;  
        playerAnim.SetTrigger("Attack1");
        enemyScript.EnemyHurt(attackStrength);
    }
}
