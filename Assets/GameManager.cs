using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int playersCreated = 0;

    [Header("Canvases")]
    public GameObject cnvsLogin;
    public GameObject cnvsBattle;
    public GameObject cnvsLevelUp;
    public GameObject cnvsBet;
    public GameObject cnvsBrowser;


    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    public enum currentScene
    {
        LOGIN,
        LEVELUP,
        BET,
        BATTLE,
        BROWSER
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeScene(currentScene.LOGIN);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void CreatePlayer()
    {
        playersCreated++;

        if (playersCreated == 2)
        {
            ChangeScene(currentScene.BET);
        }
    }

    public void ChangeScene(currentScene newScene)
    {
        switch (newScene)
        {
            case currentScene.LOGIN:
                cnvsLevelUp.SetActive(false);
                cnvsBrowser.SetActive(false);
                cnvsBet.SetActive(false);
                cnvsBattle.SetActive(false);
                this.GetComponent<LoginManager>().ShowScreen();
                cnvsLogin.SetActive(true);
                break;

            case currentScene.LEVELUP:
                cnvsLogin.SetActive(false);
                cnvsBrowser.SetActive(false);
                cnvsBet.SetActive(false);
                cnvsBattle.SetActive(false);
                this.GetComponent<LevelUpManager>().ShowScreen();
                cnvsLevelUp.SetActive(true);
                break;

            case currentScene.BROWSER:
                cnvsLogin.SetActive(false);
                cnvsLevelUp.SetActive(false);
                cnvsBet.SetActive(false);
                cnvsBattle.SetActive(false);
                this.GetComponent<BrowserManager>().ShowScreen();
                cnvsBrowser.SetActive(true);
                break;

            case currentScene.BET:
                cnvsLogin.SetActive(false);
                cnvsLevelUp.SetActive(false);
                cnvsBrowser.SetActive(false);
                cnvsBattle.SetActive(false);
                this.GetComponent<BettingManager>().ShowScreen();
                cnvsBet.SetActive(true);
                break;

            case currentScene.BATTLE:
                cnvsLogin.SetActive(false);
                cnvsLevelUp.SetActive(false);
                cnvsBrowser.SetActive(false);
                cnvsBet.SetActive(false);
                this.GetComponent<CombatManager>().ShowScreen();
                cnvsBattle.SetActive(true);
                break;
        }
    }

}
