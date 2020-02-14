using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public Button btnAttackUp, btnAttackDown;
    public Button btnDefenceUp, btnDefenceDown;
    public Button btnHealthUp, btnHealthDown;
    public Button btnSpecialUp, btnSpecialDown;
    public Button btnLuckUp, btnLuckDown;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player1 = GameObject.Find("Player1");
        btnAttackUp.onClick.AddListener(delegate { player1.GetComponent<Character>().setAttack(1); });
        btnAttackDown.onClick.AddListener(delegate { player1.GetComponent<Character>().setAttack(-1); });

        btnDefenceUp.onClick.AddListener(delegate { player1.GetComponent<Character>().setDefence(1); });
        btnDefenceDown.onClick.AddListener(delegate { player1.GetComponent<Character>().setDefence(-1); });

        btnHealthUp.onClick.AddListener(delegate { player1.GetComponent<Character>().setHealth(1); });
        btnHealthDown.onClick.AddListener(delegate { player1.GetComponent<Character>().setHealth(-1); });

        btnSpecialUp.onClick.AddListener(delegate { player1.GetComponent<Character>().setSpecial(1); });
        btnSpecialDown.onClick.AddListener(delegate { player1.GetComponent<Character>().setSpecial(-1); });

        btnLuckUp.onClick.AddListener(delegate { player1.GetComponent<Character>().setLuck(1); });
        btnLuckDown.onClick.AddListener(delegate { player1.GetComponent<Character>().setLuck(-1); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
