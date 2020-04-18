using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserManager : MonoBehaviour
{
    [Header("Text Fields")]
    public GameObject boxContent;

    public Text txtConsole;
    public GameObject defaultBtn;

    private Dictionary<string, NetworkManager.PlayerData> readyPlayersDict;

    // Start is called before the first frame update
    void Start()
    {
        readyPlayersDict = new Dictionary<string, NetworkManager.PlayerData>();
    }

    public void ShowScreen()
    {
        this.GetComponent<NetworkManager>().GetReadyPlayers();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetConsoleText(string text)
    {
        txtConsole.text = text;
    }

    public void SetReadyPlayers(List<NetworkManager.PlayerData> readyPlayers)
    {
        for (int x = 0; x < readyPlayers.Count; x++)
        {
            readyPlayersDict.Add(readyPlayers[x].user_id, readyPlayers[x]);
            GameObject btnNew = Instantiate(defaultBtn) as GameObject;
            btnNew.transform.SetParent(boxContent.transform, false);
            btnNew.GetComponentInChildren<Text>().text = readyPlayers[x].user_id;
            btnNew.GetComponent<Button>().onClick.AddListener(delegate { SetPlayer2(btnNew.GetComponentInChildren<Text>().text); this.GetComponent<NetworkManager>().JoinPlayers(); });
        }
    }

    public void SetPlayer2(string playerName)
    {
        Character player2 = this.GetComponent<GameManager>().player2.GetComponent<Character>();
        NetworkManager.PlayerData tempData = readyPlayersDict[playerName];

        this.GetComponent<GameManager>().player2.GetComponent<Character>().resetCharacter();
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setCharacterName(playerName);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setAddress(tempData.address);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setAttackLvl(tempData.attackLvl);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setDefenceLvl(tempData.defenceLvl);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setMaxHealthLvl(tempData.healthLvl);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setSpecialLvl(tempData.specialLvl);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setLuckLvl(tempData.luckLvl);
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setPoints(-player2.getPoints());
        this.GetComponent<GameManager>().player2.GetComponent<Character>().setPoints(tempData.skillPoints);

        

    }
}
