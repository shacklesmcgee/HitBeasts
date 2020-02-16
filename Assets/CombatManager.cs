using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    Character activePlayer;
    Character waitingPlayer;
    Character winner, loser;

    [Header("Buttons")]
    public Button btnAttack;
    public Button btnHeal;
    public Button btnSpecial;
    public Button btnRun;
    public Button btnExit;

    [Header("Text")]
    public Text txtConsole;
    public Text txtPlayer1Health, txtPlayer2Health;
    public Text txtBattleResultsConsole;

    public GameObject cnvs_BattleResults;

    //Alternativly, if they have to bet the same amount then we only need one variable to hold the total bet

    private int turnNum = 1;
    private int rewardPoints = 0;

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
        btnAttack.onClick.AddListener(delegate { currentplayerAction = playerAction.ATTACK; });
        btnHeal.onClick.AddListener(delegate { currentplayerAction = playerAction.HEAL; });
        btnSpecial.onClick.AddListener(delegate { currentplayerAction = playerAction.SPECIAL; });
        btnRun.onClick.AddListener(delegate { currentplayerAction = playerAction.RUN; ; });
    }

    public void ShowScreen()
    {
        //get Player IDs, create PlayerStructs (player stats and pointBet) randomly assign one to go first
        //turnNum = Random.Range(1, 2);
        turnNum = 1;
        ChangeTurn();
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

        activePlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
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
                    AttackEnemy();
                    break;

                case playerAction.HEAL:
                    HealPlayer();
                    break;

                case playerAction.SPECIAL:
                    SpecialMove();
                    break;
                case playerAction.DEFEND:
                    //do something
                    currentplayerAction = playerAction.NULL;
                    break;
                case playerAction.RUN:
                    runFromBattle();
                    break;
                case playerAction.NULL:
                    break;
          
            }
        }
    }

    public void AttackEnemy()
    {
        damageRange = activePlayer.getAttack();
        attack = Random.Range(damageRange.Item1, damageRange.Item2);

        defenceRange = activePlayer.getDefence();
        defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

        result = ((attack - defence) > 0) ? (attack - defence) : 0;
        waitingPlayer.changeCurrentHealth(result, true);

        txtConsole.text = activePlayer.getCharacterName() + " ATTACKS " + waitingPlayer.getCharacterName() + " FOR " + result + " DAMAGE";

        UpdateText();
        CheckBattleOver();
        ChangeTurn();

        currentplayerAction = playerAction.NULL;
    }

    public void HealPlayer()
    {
        damageRange = activePlayer.getHeal();
        attack = Random.Range(damageRange.Item1, damageRange.Item2);

        activePlayer.changeCurrentHealth(attack, false);
        
        if (activePlayer.getCurrentHealth() >= activePlayer.getMaxHealth())
        {
            activePlayer.setCurrentHealth(activePlayer.getMaxHealth());
        }

        txtConsole.text = activePlayer.getCharacterName() + " HEALS " + activePlayer.getCharacterName() + " FOR " + result + " HEALTH";

        UpdateText();
        CheckBattleOver();
        ChangeTurn();

        currentplayerAction = playerAction.NULL;
    }

    public void SpecialMove()
    {
        damageRange = activePlayer.getSpecial();
        attack = Random.Range(damageRange.Item1, damageRange.Item2);

        defenceRange = activePlayer.getDefence();
        defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

        result = ((attack - defence) > 0) ? (attack - defence) : 0;
        waitingPlayer.changeCurrentHealth(result, true);

        txtConsole.text = activePlayer.getCharacterName() + " SPECIAL ATTACKS " + waitingPlayer.getCharacterName() + " FOR " + result + " DAMAGE";

        UpdateText();
        CheckBattleOver();
        ChangeTurn();

        currentplayerAction = playerAction.NULL;
    }

    public void runFromBattle()
    {
        //random chance to escape fight, lose half of bet points
        UpdateText();
        ChangeTurn();
        currentplayerAction = playerAction.NULL;
    }

    public void UpdateText()
    {
        btnAttack.GetComponentInChildren<Text>().text = "ATTACK " + activePlayer.getAttack().Item1 + " - " + activePlayer.getAttack().Item2;
        btnHeal.GetComponentInChildren<Text>().text = "HEAL " + activePlayer.getHeal();
        btnSpecial.GetComponentInChildren<Text>().text = "SPECIAL " + activePlayer.getSpecial().Item1 + " - " + activePlayer.getSpecial().Item2;
        btnRun.GetComponentInChildren<Text>().text = "RUN"; ;

        txtPlayer1Health.text = this.GetComponent<GameManager>().player1.GetComponent<Character>().getCurrentHealth() + " / " + this.GetComponent<GameManager>().player1.GetComponent<Character>().getMaxHealth();
        txtPlayer2Health.text = this.GetComponent<GameManager>().player2.GetComponent<Character>().getCurrentHealth() + " / " + this.GetComponent<GameManager>().player2.GetComponent<Character>().getMaxHealth();

    }

    public void CheckBattleOver()
    {
        if (waitingPlayer.getCurrentHealth() <= 0)
        {
            cnvs_BattleResults.SetActive(true);

            //just a random number for now, would be like xp gained from battle
            rewardPoints = Random.Range(2, 5); 
            rewardPoints += activePlayer.getBetPoints() + waitingPlayer.getBetPoints(); //add the bet points

            txtBattleResultsConsole.text = activePlayer.getCharacterName() + " DEFEATED " + waitingPlayer.getCharacterName() + 
                " AND WINS " + rewardPoints + " SKILL POINTS";

            activePlayer.setPoints(rewardPoints);
            btnExit.onClick.AddListener(delegate { this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.LEVELUP); });
        }

        else if (activePlayer.getCurrentHealth() <= 0)
        {
            cnvs_BattleResults.SetActive(true);

            //just a random number for now, would be like xp gained from battle
            rewardPoints = Random.Range(2, 5);
            rewardPoints += activePlayer.getBetPoints() + waitingPlayer.getBetPoints(); //add the bet points

            txtBattleResultsConsole.text = waitingPlayer.getCharacterName() + " DEFEATED " + activePlayer.getCharacterName() +
                " AND WINS " + rewardPoints + " SKILL POINTS";

            waitingPlayer.setPoints(rewardPoints);
            btnExit.onClick.AddListener(delegate { this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.LEVELUP); });
        }
    }
}
