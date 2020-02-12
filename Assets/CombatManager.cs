using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    DummyScript activePlayer;
    DummyScript waitingPlayer;

    //Alternativly, if they have to bet the same amount then we only need one variable to hold the total bet
    int player1PointBet;
    int player2PointBet;

    enum playerAction
    {
        ATTACK,
        HEAL,
        ITEM, //could also be special
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
            activePlayer = GameObject.Find("Player1").GetComponent<DummyScript>();
            waitingPlayer = GameObject.Find("Player2").GetComponent<DummyScript>();
        }
        else
        {
            activePlayer = GameObject.Find("Player2").GetComponent<DummyScript>();
            waitingPlayer = GameObject.Find("Player1").GetComponent<DummyScript>();
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
                    waitingPlayer.modifyHealth(Random.Range(damageRange.Item1, damageRange.Item2) - waitingPlayer.getDefence(), true);
                    break;
                case playerAction.HEAL:
                    activePlayer.modifyHealth(activePlayer.getHeal(), false);
                    break;
                case playerAction.ITEM:
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
}
