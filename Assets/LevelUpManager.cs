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

    // Start is called before the first frame update
    void Start()
    {
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

    public void ShowScreen()
    {
        player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();

        txtName.text = player1.getCharacterName();

        UpdateText();
    }

    public void UpdateText()
    {
        txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        txtAttack.text = "ATTACK : " + player1.getAttack().Item1 + " - " + player1.getAttack().Item2;
        txtDefence.text = "DEFENCE : " + player1.getDefence().Item1 + " - " + player1.getDefence().Item2;
        txtMaxHealth.text = "MAX HEALTH : " + player1.getMaxHealth();
        txtSpecial.text = "SPECIAL MOVE : " + player1.getSpecial().Item1 + " - " + player1.getSpecial().Item2;
        txtLuck.text = "LUCK : " + player1.getLuck();

        txtAttackLvl.text = "LVL : " + player1.getAttackLvl();
        txtDefenceLvl.text = "LVL : " + player1.getDefenceLvl();
        txtMaxHealthLvl.text = "LVL : " + player1.getMaxHealthLvl();
        txtSpecialLvl.text = "LVL : " + player1.getSpecialLvl();
        txtLuckLvl.text = "LVL : " + player1.getLuckLvl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAttack(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) || (change < 0 && player1.getAttackLvl() > 0))
        {
            player1.setAttackLvl(change);

            txtAttack.text = "ATTACK : " + player1.getAttack().Item1 + " - " + player1.getAttack().Item2;
            txtAttackLvl.text = "LVL : " + player1.getAttackLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        }
    }

    public void ChangeDefence(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) || (change < 0 && player1.getDefenceLvl() > 0))
        {
            player1.setDefenceLvl(change);

            txtDefence.text = "DEFENCE : " + player1.getDefence().Item1 + " - " + player1.getDefence().Item2;
            txtDefenceLvl.text = "LVL : " + player1.getDefenceLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        }
    }

    public void ChangeMaxHealth(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) || (change < 0 && player1.getMaxHealthLvl() > 0))
        {
            player1.setMaxHealthLvl(change);
            player1.setCurrentHealth(player1.getMaxHealth());

            txtMaxHealth.text = "MAX HEALTH : " + player1.getMaxHealth();
            txtMaxHealthLvl.text = "LVL : " + player1.getMaxHealthLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        }
    }

    public void ChangeSpecial(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) || (change < 0 && player1.getSpecialLvl() > 0))
        {
            player1.setSpecialLvl(change);

            txtSpecial.text = "SPECIAL MOVE : " + player1.getSpecial().Item1 + " - " + player1.getSpecial().Item2;
            txtSpecialLvl.text = "LVL : " + player1.getSpecialLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        }
    }

    public void ChangeLuck(int change)
    {
        if ((change > 0 && player1.getPoints() > 0) || (change < 0 && player1.getLuckLvl() > 0))
        {
            player1.setLuckLvl(change);

            txtLuck.text = "LUCK : " + player1.getLuck();
            txtLuckLvl.text = "LVL : " + player1.getLuckLvl();
            txtPoints.text = "TOTAL SKILL POINTS : " + player1.getPoints();
        }
    }

    public void ConfirmLevelUp()
    {
        this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.BROWSER);
    }
}
