using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUI;

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    // 방에 들어오면 콜백되는 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server... ");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("WE JOINED THE LOBBY");
        roomUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomsettings = defaultRooms[defaultRoomIndex];
        
        // 씬 로드
        PhotonNetwork.LoadLevel(roomsettings.sceneIndex);
        
        // 방 생성
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = roomsettings.maxPlayer;
        roomOptions.IsVisible = true;       // 참가자들이 볼수 있다.
        roomOptions.IsOpen = true;          // 방이 열려있다.
        
        // 방과 연결
        PhotonNetwork.JoinOrCreateRoom(roomsettings.Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
