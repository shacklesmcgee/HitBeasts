using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button btnAttackUp, btnAttackDown;
    public Button btnDefenceUp, btnDefenceDown;
    public Button btnHealthUp, btnHealthDown;
    public Button btnSpecialUp, btnSpecialDown;
    public Button btnLuckUp, btnLuckDown;
    public Button btnConfirm;
    [Space(10)]

    [Header("Text Fields")]
    public Text txtName;
    public InputField inpPlaceholder;
    public Text txtPlaceholder;
    public Text txtPoints;
    public Text txtAttack;
    public Text txtDefence;
    public Text txtMaxHealth;
    public Text txtSpecial;
    public Text txtLuck;
    [Space(5)]
    public Text txtAttackLvl;
    public Text txtDefenceLvl;
    public Text txtMaxHealthLvl;
    public Text txtSpecialLvl;
    public Text txtLuckLvl;

    private Character player1;
    private Character player2;
    private Character currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();
        player2 = this.GetComponent<GameManager>().player2.GetComponent<Character>();
        currentPlayer = player1;

        txtPlaceholder.text = "Player 1 Name...";

        ResetCanvas();

        btnAttackUp.onClick.AddListener(delegate { ChangeAttack(1); });
        btnAttackDown.onClick.AddListener(delegate { ChangeAttack(-1); });

        btnDefenceUp.onClick.AddListener(delegate { ChangeDefence(1); });
        btnDefenceDown.onClick.AddListener(delegate { ChangeDefence(-1); });

        btnHealthUp.onClick.AddListener(delegate { ChangeMaxHealth(1); });
        btnHealthDown.onClick.AddListener(delegate { ChangeMaxHealth(-1); });

        btnSpecialUp.onClick.AddListener(delegate { ChangeSpecial(1); });
        btnSpecialDown.onClick.AddListener(delegate { ChangeSpecial(-1); });

        btnLuckUp.onClick.AddListener(delegate { ChangeLuck(1); });
        btnLuckDown.onClick.AddListener(delegate { ChangeLuck(-1); });

        btnConfirm.onClick.AddListener(delegate { ConfirmLevelUp(); });
    }

    void ResetCanvas()
    {
        txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        txtAttack.text = "ATTACK : " + currentPlayer.getAttack().Item1 + " - " + currentPlayer.getAttack().Item2;
        txtDefence.text = "DEFENCE : " + currentPlayer.getDefence().Item1 + " - " + currentPlayer.getDefence().Item2;
        txtMaxHealth.text = "MAX HEALTH : " + currentPlayer.getMaxHealth();
        txtSpecial.text = "SPECIAL MOVE : " + currentPlayer.getSpecial().Item1 + " - " + currentPlayer.getSpecial().Item2;
        txtLuck.text = "LUCK : " + currentPlayer.getLuck();

        txtAttackLvl.text = "LVL : " + currentPlayer.getAttackLvl();
        txtDefenceLvl.text = "LVL : " + currentPlayer.getDefenceLvl();
        txtMaxHealthLvl.text = "LVL : " + currentPlayer.getMaxHealthLvl();
        txtSpecialLvl.text = "LVL : " + currentPlayer.getSpecialLvl();
        txtLuckLvl.text = "LVL : " + currentPlayer.getLuckLvl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAttack(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || (change < 0 && currentPlayer.getAttackLvl() > 0))
        {
            currentPlayer.setAttack(change);
            currentPlayer.setAttackLvl(change);

            txtAttack.text = "ATTACK : " + currentPlayer.getAttack().Item1 + " - " + currentPlayer.getAttack().Item2;
            txtAttackLvl.text = "LVL : " + currentPlayer.getAttackLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        }
    }

    public void ChangeDefence(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || (change < 0 && currentPlayer.getDefenceLvl() > 0))
        {
            currentPlayer.setDefence(change);
            currentPlayer.setDefenceLvl(change);

            txtDefence.text = "DEFENCE : " + currentPlayer.getDefence().Item1 + " - " + currentPlayer.getDefence().Item2;
            txtDefenceLvl.text = "LVL : " + currentPlayer.getDefenceLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        }
    }

    public void ChangeMaxHealth(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || (change < 0 && currentPlayer.getMaxHealthLvl() > 0))
        {
            currentPlayer.setMaxHealth(change);
            currentPlayer.setMaxHealthLvl(change);

            txtMaxHealth.text = "MAX HEALTH : " + currentPlayer.getMaxHealth();
            txtMaxHealthLvl.text = "LVL : " + currentPlayer.getMaxHealthLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        }
    }

    public void ChangeSpecial(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || (change < 0 && currentPlayer.getSpecialLvl() > 0))
        {
            currentPlayer.setSpecial(change);
            currentPlayer.setSpecialLvl(change);

            txtSpecial.text = "SPECIAL MOVE : " + currentPlayer.getSpecial().Item1 + " - " + currentPlayer.getSpecial().Item2;
            txtSpecialLvl.text = "LVL : " + currentPlayer.getSpecialLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        }
    }

    public void ChangeLuck(int change)
    {
        if ((change > 0 && currentPlayer.getPoints() > 0) || (change < 0 && currentPlayer.getLuckLvl() > 0))
        {
            currentPlayer.setLuck(change);
            currentPlayer.setLuckLvl(change);

            txtLuck.text = "LUCK : " + currentPlayer.getLuck();
            txtLuckLvl.text = "LVL : " + currentPlayer.getLuckLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + currentPlayer.getPoints();
        }
    }

    public void ConfirmLevelUp()
    {
        if (txtName.text == "" || txtName.text == null)
        {
            inpPlaceholder.GetComponent<Image>().color = Color.red;
        }

        else
        {
            inpPlaceholder.GetComponent<Image>().color = Color.white;

            currentPlayer.setCharacterName(txtName.text);
            inpPlaceholder.text = "";
            txtPlaceholder.text = "Player 2 Name...";

            if (currentPlayer = player1)
            {
                currentPlayer = player2;
            }
            else if (currentPlayer = player2)
            {
                currentPlayer = player1;
            }

            this.GetComponent<GameManager>().CreatePlayer();
            ResetCanvas();
        }
    }
}
