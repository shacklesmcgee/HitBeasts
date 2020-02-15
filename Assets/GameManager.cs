using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int playersCreated = 0;

    [Header("Canvases")]
    public GameObject cnvsBattle;
    public GameObject cnvsLevelUp;
    public GameObject cnvsBet;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    public enum currentScene
    {
        LEVELUP,
        BET,
        BATTLE
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            case currentScene.LEVELUP:
                cnvsBattle.SetActive(false);
                cnvsBet.SetActive(false);
                cnvsLevelUp.SetActive(true);
                break;

            case currentScene.BET:
                cnvsBattle.SetActive(false);
                cnvsLevelUp.SetActive(false);
                this.GetComponent<BettingManager>().ResetCanvas();
                cnvsBet.SetActive(true);
                break;

            case currentScene.BATTLE:
                cnvsBet.SetActive(false);
                cnvsLevelUp.SetActive(false);
                cnvsBattle.SetActive(true);
                break;

        }
    }
}
