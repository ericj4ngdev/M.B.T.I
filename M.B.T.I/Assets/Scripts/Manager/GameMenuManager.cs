using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    // 플레이어 앞에 UI띄우기 위한 변수 생성
    // public Transform head;
    // public float spawnDistance = 0.5f;
    private GameObject XRPlayer;
    public GameObject menu;
    public GameObject initScreen;
    public GameObject checkBox;
    public InputActionProperty showButton;

    HashSet<string> completeChallenges = new HashSet<string>();

    private void Start()
    {
        XRPlayer = FindObjectOfType<XROrigin>().gameObject;
        InitializeChallenge();
    }

    private void InitializeChallenge()
    {
        completeChallenges = ChallengeManager.GetInstance().getCompleteChallenges();
        string currentSceneName = SceneManager.GetActiveScene().name;
        string[] sceneNameParts = currentSceneName.Split(new char[] { '_' }); // 여기서는 언더스코어를 기준으로 분할합니다
        
        foreach (var extractedName in sceneNameParts)
        {
            Debug.Log(extractedName);
        }

        // 이제 campusName을 사용하여 작업을 수행할 수 있습니다.
        string campusName = sceneNameParts[1];
        
        foreach (string challengeName in completeChallenges)
        {
            if (challengeName == campusName)
            {
                // challengeName과 씬 이름이 일치하면 활성화
                checkBox.SetActive(true);
                initScreen.SetActive(false);
                break; // 이미 일치하는 요소를 찾았으므로 나머지 요소들을 확인할 필요가 없습니다.
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);
        }
        if (showButton.action.WasPressedThisFrame())
        {
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);
        }
        // menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        // menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        // menu.transform.forward *= -1;
    }

    public void TeleportToBuildingForward(GameObject SpawnSpot)
    {
        XRPlayer.transform.position = SpawnSpot.transform.position;
    }

}