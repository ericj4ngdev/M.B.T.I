using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialSceneManager : MonoBehaviourPunCallbacks
{    
    public int maxPlayer;
    public Image loadingImage;
    public string mainSceneName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveToRoom());
        }
    }

    IEnumerator MoveToRoom()
    {
        yield return StartCoroutine(FadeOutScene());        // 로딩 씬
        LoadLevel();       // LoadLevel
        JoinRoom();        // JoinOrCreateRoom
    }

    IEnumerator FadeOutScene()
    {
        float timer = 0;
        float delay = 2f;
        float percent = 25f;
        Color targetColor = new Color(0, 0, 0, 1); // 목표 알파 값

        while (timer < delay)
        {
            timer += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(timer / percent);       // 타이머가 얼마나 진행되었는지 비율로 계산
            loadingImage.color = Color.Lerp(loadingImage.color, targetColor, lerpValue);    // 색상의 알파 값을 서서히 변경
            yield return null;
        }
    }


    private void LoadLevel()
    {
        PhotonNetwork.LoadLevel(mainSceneName);    // 불러올 씬 
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
}