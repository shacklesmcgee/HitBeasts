﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

public class NetworkManager : MonoBehaviour
{
    public UdpClient udp;

    public string myAddress;
    public Dictionary<string, GameObject> currentPlayers;
    public List<string> newPlayers, droppedPlayers;
    public GameState lastestGameState;

    public MessageType latestMessage;

    private PlayerData receivedData;
    private List<PlayerData> readyPlayersList;

    private bool received;
    private bool loginSuccessful;
    private bool gotList;
    private bool listEmpty;

    private Character player1;

    // Start is called before the first frame update
    void Start()
    {
        player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();

        received = false;
        loginSuccessful = false;
        gotList = false;

        newPlayers = new List<string>();
        droppedPlayers = new List<string>();
        currentPlayers = new Dictionary<string, GameObject>();

        readyPlayersList = new List<PlayerData>();
        udp = new UdpClient();
        udp.Connect("3.82.146.92", 12345);

        Byte[] sendBytes = Encoding.ASCII.GetBytes("connect");
        udp.Send(sendBytes, sendBytes.Length);
        udp.BeginReceive(new AsyncCallback(OnReceived), udp);

        InvokeRepeating("HeartBeat", 3, 3);
    }

    void OnDestroy()
    {
        udp.Dispose();
    }

    [Serializable]
    public struct PlayerData
    {
        public string user_id;
        public string game_id;
        public bool loggedIn;

        public int attackLvl;
        public int defenceLvl;
        public int healthLvl;
        public int specialLvl;
        public int luckLvl;
        public int skillPoints;
    }

    [Serializable]
    public class Player
    {
        public string id;
        public PlayerData playerData;
        public PlayerData[] readyPlayers;
    }

    [Serializable]
    public class GameState
    {
        public int pktID;
        public Player[] players;
    }

    [Serializable]
    public class MessageType
    {
        public commands cmd;
    }
    public enum commands
    {
        PLAYER_CONNECTED,
        GAME_UPDATE,
        PLAYER_DISCONNECTED,
        CONNECTION_APPROVED,
        LIST_OF_PLAYERS,
    };

    void OnReceived(IAsyncResult result)
    {
        // this is what had been passed into BeginReceive as the second parameter:
        UdpClient socket = result.AsyncState as UdpClient;

        // points towards whoever had sent the message:
        IPEndPoint source = new IPEndPoint(0, 0);

        // get the actual message and fill out the source:
        byte[] message = socket.EndReceive(result, ref source);

        // do what you'd like with `message` here:
        string returnData = Encoding.ASCII.GetString(message);

        latestMessage = JsonUtility.FromJson<MessageType>(returnData);

        try
        {
            switch (latestMessage.cmd)
            {
                case commands.PLAYER_CONNECTED:
                    Debug.Log("New Player Connected!");
                    //ListOfPlayers latestPlayer = JsonUtility.FromJson<ListOfPlayers>(returnData);
                    //Debug.Log(returnData);
                    //foreach (Player player in latestPlayer.players)
                    //{
                    //    newPlayers.Add(player.id);
                    //}
                    break;

                case commands.GAME_UPDATE:
                    Debug.Log("Game Updated!");
                    lastestGameState = JsonUtility.FromJson<GameState>(returnData);

                    receivedData = lastestGameState.players[0].playerData;
                    if (receivedData.user_id == null || receivedData.user_id == "")
                    {
                        receivedData.user_id = "Error: Password doesn't match or User doesn't exist!";
                        loginSuccessful = false;
                    }
                    else
                    {
                        loginSuccessful = true;
                    }

                    received = true;
                    break;

                case commands.LIST_OF_PLAYERS:
                    Debug.Log("Got the list of players!");
                    lastestGameState = JsonUtility.FromJson<GameState>(returnData);

                    for (int x = 0; x < lastestGameState.players.Length; x++)
                    {
                        receivedData = lastestGameState.players[x].playerData;
                        if (receivedData.user_id == null || receivedData.user_id == "")
                        {
                            Debug.Log("No Players Found!");
                            listEmpty = true;
                            gotList = true;
                            break;
                        }

                        
                        if (receivedData.user_id == player1.getCharacterName())
                        {
                            continue;
                        }

                        readyPlayersList.Add(receivedData);
                        listEmpty = false;
                        gotList = true;
                    }
                    break;

                case commands.PLAYER_DISCONNECTED:
                    Debug.Log("Player Disconnected!");
                    //ListOfDroppedPlayers latestDroppedPlayer = JsonUtility.FromJson<ListOfDroppedPlayers>(returnData);
                    //foreach (string player in latestDroppedPlayer.droppedPlayers)
                    //{
                    //    droppedPlayers.Add(player);
                    //}
                    break;

                case commands.CONNECTION_APPROVED:
                    Debug.Log("Connection Approved!");
                    //ListOfPlayers myPlayer = JsonUtility.FromJson<ListOfPlayers>(returnData);
                    //foreach (Player player in myPlayer.players)
                    //{
                    //    newPlayers.Add(player.id);
                    //    myAddress = player.id;
                    //}
                    break;

                default:
                    Debug.Log("Error: " + returnData);
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        // schedule the next receive operation once reading is done:
        socket.BeginReceive(new AsyncCallback(OnReceived), socket);
    }

    void HeartBeat()
    {
        Byte[] sendBytes = Encoding.ASCII.GetBytes("heartbeat");
        udp.Send(sendBytes, sendBytes.Length);
    }

    void Update()
    {
        if (received)
        {
            received = false;
            this.GetComponent<LoginManager>().SetConsoleText(receivedData.user_id);
            
            if (loginSuccessful)
            {
                Character player1 = this.GetComponent<GameManager>().player1.GetComponent<Character>();
                player1.resetCharacter();
                player1.setCharacterName(receivedData.user_id);
                player1.setAttackLvl(receivedData.attackLvl);
                player1.setDefenceLvl(receivedData.defenceLvl);
                player1.setMaxHealthLvl(receivedData.healthLvl);
                player1.setSpecialLvl(receivedData.specialLvl);
                player1.setLuckLvl(receivedData.luckLvl);
                player1.setPoints(-player1.getPoints());
                player1.setPoints(receivedData.skillPoints);
                loginSuccessful = false;
                this.GetComponent<GameManager>().ChangeScene(GameManager.currentScene.LEVELUP);  
            }
        }

        if (gotList)
        {
            gotList = false;
            if (listEmpty)
            {
                this.GetComponent<BrowserManager>().SetConsoleText("No Players Found!");
            }

            else
            {
                this.GetComponent<BrowserManager>().SetConsoleText("PLEASE PICK A USER TO FIGHT!");
                this.GetComponent<BrowserManager>().SetReadyPlayers(readyPlayersList);
            }
        }
    }

    public void LoginPlayer(string name, string password)
    {
        string data = "login," + name + "," + password;
        Byte[] sendBytes = Encoding.ASCII.GetBytes(data);
        udp.Send(sendBytes, sendBytes.Length);
    }

    public void GetReadyPlayers()
    {
        string data = "list,";
        Byte[] sendBytes = Encoding.ASCII.GetBytes(data);
        udp.Send(sendBytes, sendBytes.Length);
    }

    public void StartBetting(string name)
    {
        string data = "bet," + name;
        Byte[] sendBytes = Encoding.ASCII.GetBytes(data);
        udp.Send(sendBytes, sendBytes.Length);
    }
}