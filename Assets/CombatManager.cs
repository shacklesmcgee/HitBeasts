using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    Character activePlayer;
    Character waitingPlayer;

    //Alternativly, if they have to bet the same amount then we only need one variable to hold the total bet
    int player1PointBet;
    int player2PointBet;

    enum playerAction
    {
        ATTACK,
        HEAL,
        SPECIAL,//could also be special
        DEFEND, //if we want it
        RUN
    }

    //Dummy variable that substitutes for player input
    playerAction currentplayerAction = playerAction.ATTACK;


    void start()
    {
        //get Player IDs, create PlayerStructs (player stats and pointBet) randomly assign one to go first
        player1PointBet = 30;
        player2PointBet = 43;
        int first = Random.Range(0, 1);
        if(first == 0)
        {
            activePlayer = GameObject.Find("Player1").GetComponent<Character>();
            waitingPlayer = GameObject.Find("Player2").GetComponent<Character>();
        }
        else
        {
            activePlayer = GameObject.Find("Player2").GetComponent<Character>();
            waitingPlayer = GameObject.Find("Player1").GetComponent<Character>();
        }
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
                    (int, int) damageRange = activePlayer.getAttack();
                    int attack = Random.Range(damageRange.Item1, damageRange.Item2);

                    (int, int) defenceRange = activePlayer.getDefence();
                    int defence = Random.Range(defenceRange.Item1, defenceRange.Item2);

                    waitingPlayer.modifyHealth(attack - defence, true);
                    break;

                case playerAction.HEAL:
                    activePlayer.modifyHealth(activePlayer.getHeal(), false);
                    break;
                case playerAction.SPECIAL:
                    //do something
                    break;
                case playerAction.DEFEND:
                    //do something
                    break;
                case playerAction.RUN:
                    //random chance to escape fight, lose half of bet points
                    break;

          
            }
        }
    }

    public void AttackEnemy()
    {
        currentplayerAction = playerAction.ATTACK;
    }

    public void HealPlayer()
    {
        currentplayerAction = playerAction.HEAL;
    }

    public void Defend()
    {
        currentplayerAction = playerAction.DEFEND;
    }

    public void SpecialMove()
    {
        currentplayerAction = playerAction.SPECIAL;
    }

    public void Run()
    {
        currentplayerAction = playerAction.RUN;
    }
}
