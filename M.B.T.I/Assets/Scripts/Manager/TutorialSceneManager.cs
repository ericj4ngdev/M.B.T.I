using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviourPunCallbacks
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveToRoom();
        }
    }

    public int maxPlayer;

    public void MoveToRoom()
    {
        LoadLevel();       // LoadLevel
        JoinRoom();        // JoinOrCreateRoom
    }

    private void LoadLevel()
    {
        PhotonNetwork.LoadLevel("MBTI_Main_Net");    // 불러올 씬 
        Debug.Log("main 레벨 불러옴");
    }

    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        roomOptions.IsVisible = true;       // 참가자들이 볼수 있다.
        roomOptions.IsOpen = true;          // 방이 열려있다.

        PhotonNetwork.JoinOrCreateRoom("Main", roomOptions, TypedLobby.Default);     // Room은 가상의 이름 "main"으로 지은 것
        Debug.Log("main 방참가");
    }

    /*private void LoadNextScene()
    {
        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(LoadMyAsyncScene());
    }
    
    IEnumerator LoadMyAsyncScene()
    {    
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;
        
        timer = 0;
        float fakeLoadingDuration = 1f / fakeLoadingTime; // 페이크 로딩 시간의 역수 계산
        
        // Scene을 불러오는 것이 완료될 때까지 대기한다.
        while (!asyncLoad.isDone)
        {
            // 진행상황 확인
            if (asyncLoad.progress < 0.9f)
            {
                percent = asyncLoad.progress * 100f;
            }
            else
            {
                // 1초간 페이크 로딩
                // 페이크 로딩
                timer += Time.deltaTime * fakeLoadingDuration;
                percent = Mathf.Lerp(90f, 100f, timer);
                if (percent >= 100)
                {
                    asyncLoad.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }*/
}