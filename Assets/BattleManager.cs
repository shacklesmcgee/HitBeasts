using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    Character activePlayer, waitingPlayer;
    Character player1, player2;

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
        btnAttack.onClick.AddListener(delegate { Attack(); });
        btnHeal.onClick.AddListener(delegate { Heal(); });
        btnSpecial.onClick.AddListener(delegate { Special(); });
        btnRun.onClick.AddListener(delegate { currentplayerAction = playerAction.RUN; ; });

        btnExit.onClick.AddListener(delegate { EndBattle(); });

        player1 = GameObject.Find("Player1").GetComponent<Character>();
        player2 = GameObject.Find("Player2").GetComponent<Character>();
    }

    public void EndBattle()
    {
        this.GetComponent<NetworkManager>().EndBattle();
    }
    public void ShowScreen()
    {
        cnvs_BattleResults.SetActive(false);

        player1.setCurrentHealth(player1.getMaxHealth());
        player2.setCurrentHealth(player2.getMaxHealth());

        EnableButtons();
        //get Player IDs, create PlayerStructs (player stats and pointBet) randomly assign one to go first
        //turnNum = Random.Range(1, 2);
        turnNum = 1;
        ChangeTurn();
        UpdateText();
    }

    public void DisableButtons()
    {
        btnAttack.interactable = false;
        btnHeal.interactable = false;
        btnSpecial.interactable = false;
        btnRun.interactable = false;
    }

    public void EnableButtons()
    {
        btnAttack.interactable = true;
        btnHeal.interactable = true;
        btnSpecial.interactable = true;
        btnRun.interactable = true;
    }

    public void ChangeTurn()
    {
        if (turnNum == 1)
        {
            activePlayer = player1;
            waitingPlayer = player2;
            turnNum = 2;
        }
        else if(turnNum == 2)
        {
            activePlayer = player2;
            waitingPlayer = player1;
            turnNum = 1;
        }

        activePlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        waitingPlayer.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    // In multiplayer this will trigger on received value from the player, not every tick.
    void Update()
    {
        //if (activePlayer != null) //if player ID matches the turn cycle, process input
        //{
        //    //read the message for the player action

        //    switch (currentplayerAction)
        //    {
        //        case playerAction.ATTACK:
        //            AttackEnemy();
        //            break;

        //        case playerAction.HEAL:
        //            HealPlayer();
        //            break;

        //        case playerAction.SPECIAL:
        //            SpecialMove();
        //            break;
        //        case playerAction.DEFEND:
        //            //do something
        //            currentplayerAction = playerAction.NULL;
        //            break;
        //        case playerAction.RUN:
        //            runFromBattle();
        //            break;
        //        case playerAction.NULL:
        //            break;
          
        //    }
        //}
    }

    public void Attack()
    {
        this.GetComponent<NetworkManager>().Attack();

        //damageRange = activePlayer.getAttack();
        //attack = Random.Range(damageRange.Item1, damageRange.Item2);

        //defenceRange = activePlayer.getDefence();
        //defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

        //result = ((attack - defence) > 0) ? (attack - defence) : 0;
        //waitingPlayer.changeCurrentHealth(result, true);

       

        //UpdateText();
        //CheckBattleOver();
        //ChangeTurn();

        //currentplayerAction = playerAction.NULL;
    }

    public void Heal()
    {
        this.GetComponent<NetworkManager>().Heal();
        //damageRange = activePlayer.getHeal();
        //attack = Random.Range(damageRange.Item1, damageRange.Item2);

        //activePlayer.changeCurrentHealth(attack, false);

        //if (activePlayer.getCurrentHealth() >= activePlayer.getMaxHealth())
        //{
        //    activePlayer.setCurrentHealth(activePlayer.getMaxHealth());
        //}

        //txtConsole.text = activePlayer.getCharacterName() + " HEALS " + activePlayer.getCharacterName() + " FOR " + result + " HEALTH";

        //UpdateText();
        //CheckBattleOver();
        //ChangeTurn();

        //currentplayerAction = playerAction.NULL;
    }

    public void Special()
    {
        this.GetComponent<NetworkManager>().Special();
        //damageRange = activePlayer.getSpecial();
        //attack = Random.Range(damageRange.Item1, damageRange.Item2);

        //defenceRange = activePlayer.getDefence();
        //defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

        //result = ((attack - defence) > 0) ? (attack - defence) : 0;
        //waitingPlayer.changeCurrentHealth(result, true);

        //txtConsole.text = activePlayer.getCharacterName() + " SPECIAL ATTACKS " + waitingPlayer.getCharacterName() + " FOR " + result + " DAMAGE";

        //UpdateText();
        //CheckBattleOver();
        //ChangeTurn();

        //currentplayerAction = playerAction.NULL;
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
        //txtConsole.text = activePlayer.getCharacterName() + " BLANKED " + waitingPlayer.getCharacterName() + " FOR " + "test" + " DAMAGE";

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

            txtBattleResultsConsole.text = activePlayer.getCharacterName() + " DEFEATED " + waitingPlayer.getCharacterName() +
                " AND WINS " + rewardPoints + " FROM BATTLE AND " + (activePlayer.getBetPoints() + waitingPlayer.getBetPoints()) + " FROM BETS";

            activePlayer.setPoints(rewardPoints + (activePlayer.getBetPoints() + waitingPlayer.getBetPoints()));

            btnAttack.interactable = false;
            btnHeal.interactable = false;
            btnSpecial.interactable = false;
            btnRun.interactable = false;
        }

        else if (activePlayer.getCurrentHealth() <= 0)
        {
            cnvs_BattleResults.SetActive(true);

            //just a random number for now, would be like xp gained from battle
            rewardPoints = Random.Range(2, 5);
            rewardPoints += activePlayer.getBetPoints() + waitingPlayer.getBetPoints(); //add the bet points

            txtBattleResultsConsole.text = waitingPlayer.getCharacterName() + " DEFEATED " + activePlayer.getCharacterName() +
                " AND WINS " + rewardPoints + " SKILL POINTS";

            waitingPlayer.setPoints(rewardPoints + (activePlayer.getBetPoints() + waitingPlayer.getBetPoints()));

            btnAttack.interactable = false;
            btnHeal.interactable = false;
            btnSpecial.interactable = false;
            btnRun.interactable = false;
        }
    }
}
