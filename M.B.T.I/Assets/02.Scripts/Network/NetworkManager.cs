using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<Room> Rooms;
    private Room roomSetting;
    private ServerLogger log;
    public string previousRoomName;
    public List<Transform> spawnPoint = new List<Transform>();
    [SerializeField]
    private XROrigin player;
    public bool isMaster;

    private void Start()
    {
        // 플레이어 찾기
        player = FindObjectOfType<XROrigin>();
        log = new ServerLogger();
        previousRoomName = log.ExtractWord();
        if (previousRoomName == "Main") return;     // 메인에 해당하는 spawnPoint가 없으므로 예외 처리

        if(spawnPoint.Count == 0)
        {
            return;
        }

        foreach (var VARIABLE in spawnPoint)
        {
            string spot = VARIABLE.name;
            if (previousRoomName == spot)
            {
                player.transform.position = VARIABLE.position;
                player.transform.rotation = VARIABLE.rotation;
                break;
            }
        }
    }

    private void CheckInfo()
    {
        print("방에 있는지? : " + PhotonNetwork.InRoom);
        print("로비에 있는지? : " + PhotonNetwork.InLobby);
        print("연결됐는지? : " + PhotonNetwork.IsConnected);
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckInfo();
        }

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.JoinLobby();
        }*/
        isMaster = PhotonNetwork.IsMasterClient;
    }

    // 서버 접속
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }


    // 포탈 이벤트로 등록
    public void MoveToRoom(int roomIndex)
    {
        roomSetting = Rooms[roomIndex];
        PhotonNetwork.LeaveRoom();      // OnLeftRoom을 콜백
        log.Log(roomSetting.Name);
    }

    // 서버에 연결되면 콜백되는 함수. LeaveRoom하고서도 호출함.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server!!");
        PhotonNetwork.JoinLobby();      // 바로 로비 입장
                                        // CheckInfo();
    }

    // 로비에 입장시 콜백되는 함수
    public override void OnJoinedLobby()
    {
        Debug.Log("WE JOINED THE LOBBY");
        // PhotonNetwork.LocalPlayer.NickName
        JoinAnotherRoom();
    }

    private void JoinAnotherRoom()
    {
        if (ChallengeManager.GetInstance().IsCompleteAllChallenge())
        {
            // UI 뜨기
            SceneManager.LoadScene("MBTI_Firework(JOOHONG ver)");
            return;
        }

        LoadLevel(roomSetting);       // LoadLevel
        JoinRoom(roomSetting);        // JoinOrCreateRoom
    }

    private void LoadLevel(Room room)
    {
        PhotonNetwork.LoadLevel(room.sceneIndex); // 불러올 씬
        Debug.Log($"{room.Name}씬 불러옴");
    }

    private void JoinRoom(Room room)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = room.maxPlayer;
        roomOptions.IsVisible = true;       // 참가자들이 볼수 있다.
        roomOptions.IsOpen = true;          // 방이 열려있다.

        PhotonNetwork.JoinOrCreateRoom(room.Name, roomOptions, TypedLobby.Default);
        Debug.Log($"{room.Name}방 참가");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("A new player left the room");
    }
}
