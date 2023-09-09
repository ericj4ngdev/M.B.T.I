using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mbti
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        private ServerLogger log;

        private void Awake()
        {
            // 빌드 창 설정
            Screen.SetResolution(960, 540, false);
            log = new ServerLogger();

        }
        // 서버 접속
        public void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
            log.CleanLog();
            Debug.Log("Try Connect To Server...");
        }

        // 서버에 연결되면 콜백되는 함수
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Server!!");
            PhotonNetwork.JoinLobby();      // 로비에 바로 입장
        }

        // 로비에 입장시 콜백되는 함수
        public override void OnJoinedLobby()
        {
            Debug.Log("WE JOINED THE LOBBY");
        }

        public void JoinKmu()
        {
            SceneManager.LoadScene("MBTI_KMU");
        }
    }
}