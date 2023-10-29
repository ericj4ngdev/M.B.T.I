using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameMenuManager : MonoBehaviour
{
    // 플레이어 앞에 UI띄우기 위한 변수 생성
    private Transform head;
    public float spawnDistance = 0.5f;
    public GameObject menu;
    public GameObject initScreen;
    public GameObject checkBox;
    public GameObject startBtn;
    public GameObject endBtn;
    public InputActionProperty showButton;

    HashSet<string> completeChallenges = new HashSet<string>();


    // 플레이어 처음엔 못움직이게 하기
    private XROrigin xrOrigin;
    

    private void Start()
    {
        if (xrOrigin == null) xrOrigin = FindObjectOfType<XROrigin>();
        head = xrOrigin.Camera.transform;
        SetMovement(false);
        InitializeChallenge();
    }


    private void InitializeChallenge()
    {
        completeChallenges = ChallengeManager.GetInstance().getCompleteChallenges();    // 완료한 캠퍼스 이름 HashSet
        string currentSceneName = SceneManager.GetActiveScene().name;   // 현재 컷씬 이름
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
                // challengeName과 씬 이름이 일치하면 완료한 곳이므로 메뉴 안띄움
                // 이동이 자유로움
                menu.SetActive(false);
                checkBox.SetActive(true);
                startBtn.SetActive(false);
                endBtn.SetActive(true);
                initScreen.SetActive(false);
                SetMovement(true);
                break; // 이미 일치하는 요소를 찾았으므로 나머지 요소들을 확인할 필요가 없습니다.
            }      
        }
        // 여기에 도달했다면 아직 클리어하지 않은 캠퍼스이므로 움직임 비활성화

    }

    void SetComponentEnabled<T>(bool isEnabled) where T : MonoBehaviour
    {
        MonoBehaviour componentToDisable = xrOrigin.GetComponent<T>();
        componentToDisable.enabled = isEnabled;
    }

    // 시작 버튼 이벤트
    public void SetMovement(bool canMove)
    {
        // SetComponentEnabled<ActionBasedContinuousMoveProvider>(canMove);
        // 컴포넌트 비활성화하면 Null오류가 떠서 속도를 0으로 만들어버리려 한다. 
        var move = xrOrigin.GetComponent<ActionBasedContinuousMoveProvider>();
        move.moveSpeed = canMove ? 2 : 0;
        var turn = xrOrigin.GetComponent<ActionBasedContinuousTurnProvider>();
        turn.turnSpeed = canMove ? 60 : 0;
        SetComponentEnabled<TeleportationProvider>(canMove);
        // SetComponentEnabled<ActivateTeleportationRay>(canMove);
    }

    void Update()
    {
        // 키보드용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);
        }

        if (showButton.action.WasPressedThisFrame())
        {
            // 메뉴 활성화
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);
            startBtn.SetActive(false);      // 처음 메뉴 누르면 시작버튼도 사라짐
            endBtn.SetActive(true);
            SetMovement(true);
            // 플레이어 앞에 띄우기
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    public void TeleportToBuildingForward(GameObject SpawnSpot)
    {
        xrOrigin.transform.position = SpawnSpot.transform.position;
    }

}