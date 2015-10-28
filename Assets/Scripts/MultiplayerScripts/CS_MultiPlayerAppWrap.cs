using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using com.shephertz.app42.gaming.multiplayer.client.events;
using MiniJSON;

public class CS_MultiPlayerAppWrap : MonoBehaviour
{



    public Dictionary<string, GameObject> unityUIObjects;
    public Dictionary<string, GameObject> unityStatusBarObjects;
    public bool isUserCreated;
    public bool isUserConnected;
    public string userName;
    private string roomId;
    private int index;
    private List<string> rooms;
    private int turnTime = 120;
    public string opponentName = "";
    public bool isMyTurn;

    public static Define_Vars.Player currentPlayer;

    public static int playerOneLife = 0;
    public static int playerTwoLife = 0;
    private bool isGameOver;

    public GameObject tank1;
    public GameObject tank2;

    public static bool freeze;

    static CS_MultiPlayerAppWrap _instance;


    public static CS_MultiPlayerAppWrap Instance // singletone
    {
        get
        {
            if (_instance == null) _instance = GameObject.Find("CS_MultiPlayerAppWrap").GetComponent<CS_MultiPlayerAppWrap>();

            return _instance;
        }
    }


    void OnEnable()
    {
        SC_Listener_AppWarp.OnGameStarted += OnGameStarted;
        SC_Listener_AppWarp.OnGameStopped += OnGameStopped;
        SC_Listener_AppWarp.OnMoveCompleted += OnMoveCompleted;
        SC_Listener_App42.onCreatedUserApp42 += OnCreatedUserApp42;
        SC_Listener_App42.OnExceptionFromApp42 += OnExceptionFromApp42;
        SC_Listener_AppWarp.onConnectToAppWarp += onConnectToAppWarp;
        SC_Listener_AppWarp.onDisconnectFromAppWarp += onDisconnectFromAppWarp;
        SC_Listener_AppWarp.OnMatchedRooms += OnGetMatchedRoomsDone;
        SC_Listener_AppWarp.onGetLiveRoomInfo += OnGetLiveRoomInfo;
        SC_Listener_AppWarp.OnCreateRoomDone += OnCreateRoomDone;
        SC_Listener_AppWarp.OnJoinToRoom += OnJoinToRoom;
        SC_Listener_AppWarp.OnUserJoinRoom += OnUserJoinRoom;
        SC_Listener_AppWarp.OnUserLeftRoom += OnUserLeftRoom;
        SC_Listener_AppWarp.OnPrivateChatReceived += OnPrivateChatReceived;
    }


    void OnDisable()
    {
        SC_Listener_AppWarp.OnGameStarted -= OnGameStarted;
        SC_Listener_AppWarp.OnGameStopped -= OnGameStopped;
        SC_Listener_AppWarp.OnMoveCompleted -= OnMoveCompleted;
        SC_Listener_App42.onCreatedUserApp42 -= OnCreatedUserApp42;
        SC_Listener_App42.OnExceptionFromApp42 -= OnExceptionFromApp42;
        SC_Listener_AppWarp.onConnectToAppWarp -= onConnectToAppWarp;
        SC_Listener_AppWarp.onDisconnectFromAppWarp -= onDisconnectFromAppWarp;
        SC_Listener_AppWarp.OnMatchedRooms -= OnGetMatchedRoomsDone;
        SC_Listener_AppWarp.onGetLiveRoomInfo -= OnGetLiveRoomInfo;
        SC_Listener_AppWarp.OnCreateRoomDone -= OnCreateRoomDone;
        SC_Listener_AppWarp.OnJoinToRoom -= OnJoinToRoom;
        SC_Listener_AppWarp.OnUserJoinRoom -= OnUserJoinRoom;
        SC_Listener_AppWarp.OnUserLeftRoom -= OnUserLeftRoom;
        SC_Listener_AppWarp.OnPrivateChatReceived -= OnPrivateChatReceived;
    }

    // Use this for initialization
    void Start()
    {

        Init();


    }


    void Update()
    {
        InitStatusBar(Define_Vars.bulletSpeedForJson, Define_Vars.bulletAngleForJson);//init score and player life
        if (playerOneLife <= 0 || playerTwoLife <= 0) isGameOver = true; // check score status
        if (isGameOver == true)
            WinnerMenu();
    }

    void Init()
    {
        unityUIObjects = new Dictionary<string, GameObject>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("UnityUIObjects");
        foreach (GameObject g in objects)
            unityUIObjects.Add(g.name, g);

        unityUIObjects["LoadingBoard"].SetActive(true);
        unityUIObjects["LoadingText"].GetComponent<Text>().text = "Connecting to Appwrap...";

        tank1 = GameObject.Find("Tank_1");
        tank2 = GameObject.Find("Tank_2");

        playerOneLife = 3;
        playerTwoLife = 3;
        freeze = false;
        isGameOver = false;

        userName = "Moshe" + ((int)(Time.time * 100000)).ToString();
        print(userName);
        SC_App42Kit.App42Init(Define_Vars.apiKey, Define_Vars.secretKey);
        //Init Server
        SC_AppWarpKit.WarpInit(Define_Vars.apiKey, Define_Vars.secretKey);

        try
        {
            Debug.Log("Connecting to Appwrap...");
            SC_AppWarpKit.connectToAppWarp(userName);
        }
        catch (Exception e)
        {
            Debug.Log(e.Data.ToString());
        }
    }



    public void OnCreatedUserApp42(object respond)
    {
        Debug.Log(respond);
        try
        {
            SC_AppWarpKit.connectToAppWarp(userName);
        }
        catch (Exception e)
        {
            print(e.Message);
            Application.LoadLevel("MultiPlayer_sence");
        }
    }

    public void OnExceptionFromApp42(Exception error)
    {
        Debug.Log("onConnectToApp42: " + error.Message);
    }

    public void onConnectToAppWarp(ConnectEvent eventObj)
    {
        Debug.Log("onConnectToAppWarp " + eventObj.getResult());
        if (eventObj.getResult() == 0)
        {
            unityUIObjects["LoadingText"].GetComponent<Text>().text = "Finding a room...";
            isUserConnected = true;
            SC_AppWarpKit.GetRoomsInRange(1, 2);
        }
        else
        {
            isUserConnected = false;
        }
    }

    public void onDisconnectFromAppWarp(ConnectEvent eventObj)
    {
        isUserConnected = false;
        Debug.Log("onDisconnectFromAppWarp " + eventObj.getResult());
    }

    public void OnGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
    {
        Debug.Log("OnGetMatchedRoomsDone : " + eventObj.getResult());
        if (isUserConnected)
        {
            rooms = new List<string>();

            foreach (var roomData in eventObj.getRoomsData())
            {
                Debug.Log("Room ID:" + roomData.getId() + ", " + roomData.getRoomOwner());
                rooms.Add(roomData.getId()); // add to the list of rooms id
            }

            index = 0;
            if (index < rooms.Count)
            {
                SC_AppWarpKit.GetLiveRoomInfo(rooms[index]);
            }
            else
            {
                Debug.Log("No Rooms");
                unityUIObjects["LoadingText"].GetComponent<Text>().text = "Creating room...";
                SC_AppWarpKit.CreateTurnBaseRoom("WarRoom" + Time.time, userName, 2, null, turnTime);
            }
        }
    }

    public void OnGetLiveRoomInfo(LiveRoomInfoEvent eventObj)
    {
        if (isUserConnected)
        {
            Debug.Log("OnGetLiveRoomInfo " + eventObj.getResult() + " " + eventObj.getData().getId() + " " + eventObj.getJoinedUsers().Length);
            if (eventObj.getResult() == 0 && eventObj.getJoinedUsers().Length == 1)
            {
                Debug.Log("Joined room " + eventObj.getData().getId());
                unityUIObjects["LoadingText"].GetComponent<Text>().text = "Joning room...";
                roomId = eventObj.getData().getId();
                SC_AppWarpKit.JoinToRoom(eventObj.getData().getId());
                SC_AppWarpKit.RegisterToRoom(eventObj.getData().getId());
            }
            else
            {
                Debug.Log("Still Looking");
                index++;
                if (index < rooms.Count)
                    SC_AppWarpKit.GetLiveRoomInfo(rooms[index]);
                else
                {
                    Debug.Log("No More Rooms");
                    unityUIObjects["LoadingText"].GetComponent<Text>().text = "Creating room...";
                    SC_AppWarpKit.CreateTurnBaseRoom("WarRoom" + Time.time, userName, 2, null, turnTime);
                }
            }
        }
    }

    public void OnCreateRoomDone(RoomEvent eventObj)
    {
        if (isUserConnected)
        {
            Debug.Log("OnCreateRoomDone " + eventObj.getResult() + eventObj.getData().getId() + " " + eventObj.getData().getRoomOwner());
            if (eventObj.getResult() == 0)
            {
                unityUIObjects["LoadingText"].GetComponent<Text>().text = "Waiting for opponent...";
                roomId = eventObj.getData().getId();
                SC_AppWarpKit.JoinToRoom(eventObj.getData().getId());
                SC_AppWarpKit.RegisterToRoom(eventObj.getData().getId());
            }

        }

    }

    public void OnUserJoinRoom(RoomData eventObj, string _UserName)
    {
        if (isUserConnected)
        {
            Debug.Log("OnUserJoinRoom" + " " + eventObj.getRoomOwner() + " User connected" + userName);
            if (_UserName != eventObj.getRoomOwner() && userName == eventObj.getRoomOwner())
            {
                opponentName = _UserName;
                unityUIObjects["LoadingText"].GetComponent<Text>().text = "Starting Game...";
                Debug.Log("Start Game");
                SC_AppWarpKit.StartGame();
                Debug.Log("opponentName: " + opponentName);

            }

            if (_UserName != eventObj.getRoomOwner())
                opponentName = eventObj.getRoomOwner();
        }


    }

    public void OnUserLeftRoom(RoomData eventObj, string _UserName)
    {
        Debug.Log("OnUserLeftRoom : " + eventObj.getName());
    }

    public void OnJoinToRoom(RoomEvent eventObj)//join roome
    {
        if (isUserConnected)
        {
            Debug.Log("OnJoinToRoom " + eventObj.getResult());
            if (eventObj.getResult() == 0)
            {
                unityUIObjects["LoadingText"].GetComponent<Text>().text = "Waiting for Game...";
                Debug.Log("Joined Room! " + eventObj.getData().getId());
            }
            else
            {
                SC_AppWarpKit.JoinToRoom(roomId);
                SC_AppWarpKit.RegisterToRoom(roomId);
            }
        }
    }

    void OnApplicationQuit()
    {
        SC_AppWarpKit.DisconnectFromAppWarp();
    }

    public void OnPrivateChatReceived(string sender, string message)
    {
        Debug.Log("OnPrivateChatReceived: " + sender);
        if (message == "YouStart")
        {
            SC_AppWarpKit.StartGame();
        }
    }


    public void OnGameStarted(string sender, string roomId, string nextTurn)
    {
        Debug.Log("OnGameStarted" + ", sender: " + sender + ", roomId: " + roomId + ", nextTurn: " + nextTurn + ", userName: " + currentPlayer);
        unityUIObjects["LoadingBoard"].SetActive(false);

        if (nextTurn == userName)
        {
            isMyTurn = true;
            currentPlayer = Define_Vars.Player.Two;

        }
        else
        {
            isMyTurn = false;
            currentPlayer = Define_Vars.Player.One;
        }
    }

    public void OnGameStopped(string sender, string roomId)
    {
        Debug.Log("OnGameStopped" + ", sender: " + sender + ", roomId: " + roomId + ", userName: ");//+ SC_MenuTicTacToe.Instance.userName);
    }

    public void OnMoveCompleted(MoveEvent move)
    {
        Debug.Log("Data: " + move.getMoveData());
        Debug.Log("OnMoveCompleted" + ", Next turn: " + move.getNextTurn() + ", Data: " + move.getMoveData() + " current Time: " + Time.time);

        if (move.getNextTurn() == userName)
        {
            Dictionary<string, object> _recData = Json.Deserialize(move.getMoveData()) as Dictionary<string, object>;
            OpponentLogic(_recData);
            isMyTurn = true;
        }
        else
        {
            isMyTurn = false;
        }
    }


    public void PlayerLogic(int bulletSpeedForJson, float bulletAngleForJson, Vector3 tankPositionForJson)//translate moves to json
    {
        if (isMyTurn == true)
        {
            Debug.Log("PlayerLogic");
            Dictionary<string, object> _sendData = new Dictionary<string, object>();
            _sendData.Add("bulletSpeedForJson", bulletSpeedForJson);
            _sendData.Add("bulletAngleForJson", bulletAngleForJson);
            _sendData.Add("tankPositionForJsonX", tankPositionForJson.x);
            _sendData.Add("tankPositionForJsonY", tankPositionForJson.y);
            _sendData.Add("tankPositionForJsonZ", tankPositionForJson.z);


            string _sendDataText = Json.Serialize(_sendData);

            //Send move - Change turn to next player and sending the last move
            SC_AppWarpKit.sendMove(_sendDataText);
        }
    }

    public void OpponentLogic(Dictionary<string, object> _recData)
    {
        print("OpponentLogic");
        Vector3 tankPositionForJson;
        tankPositionForJson.x = float.Parse(_recData["tankPositionForJsonX"].ToString());
        tankPositionForJson.y = float.Parse(_recData["tankPositionForJsonY"].ToString());
        tankPositionForJson.z = float.Parse(_recData["tankPositionForJsonZ"].ToString());

        int bulletSpeed = int.Parse(_recData["bulletSpeedForJson"].ToString());
        float bulletAngle = float.Parse(_recData["bulletAngleForJson"].ToString());

        print(bulletAngle);
        print(bulletSpeed);

        if (currentPlayer == Define_Vars.Player.One)
        {
            print("one");
            tank2.transform.position = tankPositionForJson;
            CS_multiPlayerTank2FireController.Instance.Aim(bulletAngle);
            CS_multiPlayerTank2FireController.Instance.FireAction(bulletSpeed);
        }
        else if (currentPlayer == Define_Vars.Player.Two)
        {
            print("two");
            tank1.transform.position = tankPositionForJson;
            CS_multiPlayerTank1FireController.Instance.Aim(bulletAngle);
            CS_multiPlayerTank1FireController.Instance.FireAction(bulletSpeed);

        }

    }

    public void InitStatusBar(float bulletSpeed, float bulletAngle)
    { // status var info
        unityStatusBarObjects = new Dictionary<string, GameObject>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("StatusBarObject");
        foreach (GameObject g in objects)
            unityStatusBarObjects.Add(g.name, g);
        unityStatusBarObjects["Power"].GetComponent<Text>().text = "Power: " + (bulletSpeed / 5000);
        unityStatusBarObjects["Engle"].GetComponent<Text>().text = "Engle: " + (bulletAngle);
        unityStatusBarObjects["CurrentPlayer"].GetComponent<Text>().text = "current player: " + currentPlayer;
        unityStatusBarObjects["PlayerOneHealth"].GetComponent<Text>().text = "Health: " + playerOneLife.ToString();
        unityStatusBarObjects["PlayerTwoHealth"].GetComponent<Text>().text = "Health: " + playerTwoLife.ToString();

    }

    public void WinnerMenu()
    {
        Define_Vars.Winner = currentPlayer;
        Debug.Log("WinnerMenu");
        Application.LoadLevel("Winner_sence");

    }
}