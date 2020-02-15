using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    Character activePlayer;
    Character waitingPlayer;

    [Header("Buttons")]
    public Button btnAttack;
    public Button btnHeal;
    public Button btnSpecial;
    public Button btnDefend;

    [Header("Text")]
    public Text txtConsole;
    public Text txtPlayer1Health, txtPlayer2Health;


    //Alternativly, if they have to bet the same amount then we only need one variable to hold the total bet
    int player1PointBet;
    int player2PointBet;

    private int turnNum = 1;

    private (int, int) damageRange;
    private (int, int) defenceRange;
    private int attack, defence, result;

    enum playerAction
    {
        ATTACK,
        HEAL,
        SPECIAL,//could also be special
        DEFEND, //if we want it
        RUN,
        NULL
    }

    //Dummy variable that substitutes for player input
    playerAction currentplayerAction = playerAction.NULL;


    void Start()
    {
        //get Player IDs, create PlayerStructs (player stats and pointBet) randomly assign one to go first
        turnNum = Random.Range(1, 2);
        ChangeTurn();

        btnAttack.onClick.AddListener(delegate { AttackEnemy(); });
        btnHeal.onClick.AddListener(delegate { HealPlayer(); });
        btnSpecial.onClick.AddListener(delegate { SpecialMove(); });
        btnDefend.onClick.AddListener(delegate { Defend(); });

        UpdateText();
    }

    public void ChangeTurn()
    {
        if (turnNum == 1)
        {
            activePlayer = GameObject.Find("Player1").GetComponent<Character>();
            waitingPlayer = GameObject.Find("Player2").GetComponent<Character>();
            turnNum = 2;
        }
        else if(turnNum == 2)
        {
            activePlayer = GameObject.Find("Player2").GetComponent<Character>();
            waitingPlayer = GameObject.Find("Player1").GetComponent<Character>();
            turnNum = 1;
        }

        activePlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        waitingPlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    // In multiplayer this will trigger on received value from the player, not every tick.
    void Update()
    {
        if (activePlayer != null) //if player ID matches the turn cycle, process input
        {
            //read the message for the player action

            switch (currentplayerAction)
            {

                case playerAction.ATTACK:
                    damageRange = activePlayer.getAttack();
                    attack = Random.Range(damageRange.Item1, damageRange.Item2);

                    defenceRange = activePlayer.getDefence();
                    defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

                    result = ((attack - defence) > 0) ? (attack - defence) : 0;
                    waitingPlayer.setCurrentHealth(result, true);

                    txtConsole.text = activePlayer.getCharacterName() + " ATTACKS " + waitingPlayer.getCharacterName() + " FOR " + result + " DAMAGE";
                    UpdateText();

                    currentplayerAction = playerAction.NULL;
                    break;

                case playerAction.HEAL:
                    activePlayer.setCurrentHealth(activePlayer.getHeal(), false);
                    txtConsole.text = activePlayer.getCharacterName() + " ATTACKS " + waitingPlayer.getCharacterName() + " FOR " + result + " DAMAGE";
                    UpdateText();
                    currentplayerAction = playerAction.NULL;
                    break;
                case playerAction.SPECIAL:
                    //do something
                    damageRange = activePlayer.getSpecial();
                    attack = Random.Range(damageRange.Item1, damageRange.Item2);

                    defenceRange = activePlayer.getDefence();
                    defence = Random.Range(defenceRange.Item1, defenceRange.Item2);
                    waitingPlayer.setCurrentHealth(((attack - defence) > 0) ? (attack - defence) : 0, true);
                    UpdateText();

                    currentplayerAction = playerAction.NULL;
                    break;
                case playerAction.DEFEND:
                    //do something
                    UpdateText();
                    currentplayerAction = playerAction.NULL;
                    break;
                case playerAction.RUN:
                    //random chance to escape fight, lose half of bet points
                    UpdateText();
                    currentplayerAction = playerAction.NULL;
                    break;
                case playerAction.NULL:
                    break;
          
            }
        }
    }

    public void AttackEnemy()
    {
        currentplayerAction = playerAction.ATTACK;
        ChangeTurn();
    }

    public void HealPlayer()
    {
        currentplayerAction = playerAction.HEAL;
        ChangeTurn();
    }

    public void Defend()
    {
        currentplayerAction = playerAction.DEFEND;
        ChangeTurn();
    }

    public void SpecialMove()
    {
        currentplayerAction = playerAction.SPECIAL;
        ChangeTurn();
    }

    public void Run()
    {
        currentplayerAction = playerAction.RUN;
        ChangeTurn();
    }

    public void UpdateText()
    {
        btnAttack.GetComponentInChildren<Text>().text = "ATTACK " + activePlayer.getAttack().Item1 + " - " + activePlayer.getAttack().Item2;
        btnHeal.GetComponentInChildren<Text>().text = "HEAL " + activePlayer.getHeal();
        btnSpecial.GetComponentInChildren<Text>().text = "SPECIAL " + activePlayer.getSpecial().Item1 + " - " + activePlayer.getSpecial().Item2;
        btnDefend.GetComponentInChildren<Text>().text = "DEFEND " + activePlayer.getDefence().Item1 + " - " + activePlayer.getDefence().Item2;

        txtPlayer1Health.text = this.GetComponent<GameManager>().player1.GetComponent<Character>().getCurrentHealth() + " / " + this.GetComponent<GameManager>().player1.GetComponent<Character>().getMaxHealth();
        txtPlayer2Health.text = this.GetComponent<GameManager>().player2.GetComponent<Character>().getCurrentHealth() + " / " + this.GetComponent<GameManager>().player2.GetComponent<Character>().getMaxHealth();

    }
}
