using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{

    GameObject spawnedPlayerPrefab;

    // �� ������ ȣ��
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        // spawnedPlayerPrefab = PhotonNetwork.Instantiate("XROrigin", transform.position, transform.rotation);
        // spawnedPlayerPrefab = PhotonNetwork.Instantiate("XROrigin_Rigid", transform.position, transform.rotation);        
    }

    // �� ����� ȣ��
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
