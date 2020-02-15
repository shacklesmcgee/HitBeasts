using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button btnBetUp;
    public Button btnBetDown;
    public Button btnConfirmBet;
    public Button btnStartGame;
    [Space(10)]

    [Header("Text Fields")]
    public Text txtNames;
    public Text txtPoints;
    public Text txtBetPoints;
    public Text txtPlayer1Bet;
    public Text txtPlayer2Bet;

    private Character player1;
    private Character player2;
    private Character currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();
        player2 = this.GetComponent<GameManager>().player2.GetComponent<Character>();
        currentPlayer = player1;

        btnBetUp.onClick.AddListener(delegate { ChangeBet(1); });
        btnBetDown.onClick.AddListener(delegate { ChangeBet(-1); });
        btnConfirmBet.onClick.AddListener(delegate { ConfirmBet(); });
        btnStartGame.onClick.AddListener(delegate { this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.BATTLE); });

        ResetCanvas();

        btnStartGame.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBet(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || 
            (change < 0 && currentPlayer.getBetPoints() > 0))
        {
            currentPlayer.setBetPoints(change);
            currentPlayer.setPoints(-change);

            txtPlayer1Bet.text = "PLAYER 1 BETS : " + player1.getBetPoints();
            txtPlayer2Bet.text = "PLAYER 2 BETS : " + player2.getBetPoints();

            txtPoints.text = "SKILL POINTS : " + currentPlayer.getPoints();
            txtBetPoints.text = "# OF SKILL POINTS TO BET : " + currentPlayer.getBetPoints();
        }
    }

    public void ConfirmBet()
    {
        

        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else if (currentPlayer == player2)
        {
            currentPlayer = player1;
        }

        btnConfirmBet.GetComponentInChildren<Text>().text = currentPlayer.getCharacterName() + " CONFIRM YOUR BET";

        if (player1.getBetPoints() == player2.getBetPoints())
        {
            btnStartGame.interactable = true;
        }
    }

    public void ResetCanvas()
    {

        txtNames.text = "PLACE YOUR BETS " + currentPlayer.getCharacterName();
        txtPoints.text = "SKILL POINTS : " + currentPlayer.getPoints();
        txtBetPoints.text = "# OF SKILL POINTS TO BET : " + currentPlayer.getBetPoints();

        txtPlayer1Bet.text = player1.getCharacterName() + " BETS : " + player1.getBetPoints();
        txtPlayer2Bet.text = player2.getCharacterName() + " BETS : " + player2.getBetPoints();

        btnConfirmBet.GetComponentInChildren<Text>().text = currentPlayer.getCharacterName() + " CONFIRM YOUR BET";

    }
}
