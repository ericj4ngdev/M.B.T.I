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

    // �濡 ������ �ݹ�Ǵ� �Լ�
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
        
        // �� �ε�
        PhotonNetwork.LoadLevel(roomsettings.sceneIndex);
        
        // �� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = roomsettings.maxPlayer;
        roomOptions.IsVisible = true;       // �����ڵ��� ���� �ִ�.
        roomOptions.IsOpen = true;          // ���� �����ִ�.
        
        // ��� ����
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
