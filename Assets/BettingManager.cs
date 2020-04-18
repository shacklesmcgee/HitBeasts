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
        btnBetUp.onClick.AddListener(delegate { ChangeBet(1); });
        btnBetDown.onClick.AddListener(delegate { ChangeBet(-1); });
        btnConfirmBet.onClick.AddListener(delegate { ConfirmBet(); });
        btnStartGame.onClick.AddListener(delegate { this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.BATTLE); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBet(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) ||
            (change < 0 && player1.getBetPoints() > 0))
        {
            player1.setBetPoints(change);
            player1.setPoints(-change);

            UpdateText();
        }

        if (player1.getBetPoints() == player2.getBetPoints())
        {
            btnStartGame.interactable = true;
        }

        else
        {
            btnStartGame.interactable = false;
        }
    }

    public void ConfirmBet()
    {
        this.GetComponent<NetworkManager>().SendBet(player1.getBetPoints());
    }

    public void ShowScreen()
    {
        player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();
        player2 = this.GetComponent<GameManager>().player2.GetComponent<Character>();

        UpdateText();

        btnStartGame.interactable = false;
    }

    public void UpdateText()
    {
        txtNames.text = "PLACE YOUR BETS " + player1.getCharacterName();
        txtPoints.text = "SKILL POINTS : " + player1.getPoints();
        txtBetPoints.text = "# OF SKILL POINTS TO BET : " + player1.getBetPoints();

        txtPlayer1Bet.text = player1.getCharacterName() + " BETS : " + player1.getBetPoints();
        txtPlayer2Bet.text = player2.getCharacterName() + " BETS : " + player2.getBetPoints();

        btnConfirmBet.GetComponentInChildren<Text>().text = player1.getCharacterName() + " CONFIRM YOUR BET";
    }
}
