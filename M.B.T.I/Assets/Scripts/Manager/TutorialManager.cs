using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Message")]
    // [TextArea] public string[] m_wellcomeTextData;
    // [TextArea] public string[] m_tutorialTextData;
    // [TextArea] public string[] m_tabletTextData;

    [Header("UI Image")] 
    public Sprite[] m_wellcomeImgData;
    public Sprite[] m_tutorialImgData;
    public Sprite[] m_tabletImgData;
    
    [FormerlySerializedAs("wellcomeTextIndex")] 
    [Header("Debug")]
    public int wellcomeUIIndex;
    [FormerlySerializedAs("tutorialTextIndex")] public int tutorialUIIndex;
    [FormerlySerializedAs("tabletTextIndex")] public int tabletUIIndex;
    public int curIndex;
    // public string ShowText;
    public Image showUI;
    
    // public TextMeshProUGUI npcText;
    public float delay = 5f;
    public float temp = 0;
    public bool isBtn1Pressed;
    public bool isBtn2Pressed;
    public bool isPressed;
    public bool isGrab;
    public bool isPortal;
    
    [Header("Key")]
    public InputActionProperty btnA;
    public InputActionProperty btnB;
    public InputActionProperty btnX;
    public InputActionProperty btnY;
    public InputActionProperty btnLGrip;
    public InputActionProperty btnRGrip;
    public InputActionProperty btnLTrigger;
    public InputActionProperty btnRTrigger;
    public InputActionProperty btnLThumbStick;
    public InputActionProperty btnRThumbStick;

    [Header("XROrigin")] 
    public GameObject leftHand;
    public GameObject leftController;
    public GameObject rightHand;
    public GameObject rightController;
    public List<GameObject> L_Button = new List<GameObject>();
    public List<GameObject> R_Button = new List<GameObject>();
    private XROrigin xrOrigin;
    private PlayerController playercontroller;
    
    [Header("Tutorial Object")]
    public GameObject vrButton;
    public List<GameObject> grabableCube = new List<GameObject>();
    public List<TicketGate> toolTip = new List<TicketGate>();
    public GameObject tablet;
    public GameObject portal;
    
    private List<XRGrabInteractable> grabbable = new List<XRGrabInteractable>();
    
    
    void SetComponentEnabled<T>(bool isEnabled) where T : MonoBehaviour
    {
        MonoBehaviour componentToDisable = xrOrigin.GetComponent<T>();
        componentToDisable.enabled = isEnabled;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // 시작하자마자 조작키 비활성화
        if (xrOrigin == null) xrOrigin = FindObjectOfType<XROrigin>();
        SetComponentEnabled<ActionBasedContinuousMoveProvider>(false);
        SetComponentEnabled<ActionBasedContinuousTurnProvider>(false);
        SetComponentEnabled<TeleportationProvider>(false);
        SetComponentEnabled<ActivateTeleportationRay_Tuto>(false);
        // SetComponentEnabled<ActivateGrabRay>(false);
        playercontroller = xrOrigin.GetComponent<PlayerController>();
        
        foreach (var VARIABLE in L_Button)
        {
            VARIABLE.SetActive(false);
        }
        foreach (var VARIABLE in R_Button)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in grabableCube)
        {
            grabbable.Add(VARIABLE.GetComponent<XRGrabInteractable>());
        }
        
        curIndex = 0;
        wellcomeUIIndex = m_wellcomeImgData.Length;
        tutorialUIIndex = m_tutorialImgData.Length;
        tabletUIIndex = m_tabletImgData.Length;
        temp = 0;
        // ShowText = m_wellcomeImgData[curIndex];
        // npcText.text = ShowText;
        showUI.sprite = m_wellcomeImgData[curIndex];
        isBtn1Pressed = false;
        isBtn2Pressed = false;
        isPressed = false;
        isGrab = false;
        StartCoroutine(WellcomeSentence_Play());
    }
    
    // 컨트롤러 -> 손
    void ChangeController()
    {
        leftHand.SetActive(true);
        leftController.SetActive(false);
        rightHand.SetActive(true);
        rightController.SetActive(false);
    }
    
    void SpawnCube()
    {
        foreach (var VARIABLE in grabableCube)
        {
            VARIABLE.SetActive(true);
        }
    }
    
    /*void tutorialNextSentence(SelectEnterEventArgs arg)
    {
        // 다음 UI띄우기
        curIndex++;
        ShowText = m_tutorialTextData[curIndex];
        npcText.text = ShowText;
    }*/

    public void PressBtn() => isPressed = true;
    void OnGrab(SelectEnterEventArgs arg) => isGrab = true;
    void OnRelease(SelectExitEventArgs arg) => isGrab = false;
    void tabletNextSentence() => StartCoroutine(tabletSentence_Play());
    
    #region WelcomeFlow

    IEnumerator WellcomeSentence_Play()
    {
        int idx = 0;
        while (idx < wellcomeUIIndex)
        {
            showUI.sprite = m_wellcomeImgData[idx];
            // ShowText = m_wellcomeTextData[idx];
            // npcText.text = ShowText;
            idx++;
            yield return new WaitForSeconds(3f);
        }

        while (idx == wellcomeUIIndex)
        {
            // if(Input.GetKeyDown(KeyCode.Keypad1))
            if (btnA.action.WasPressedThisFrame())
            {
                // tutorialNextSentence();
                yield return StartCoroutine(tutorialSentence_Play());
                Debug.Log("A누름");
                break;
            }
            // if(Input.GetKeyDown(KeyCode.Keypad2))
            if (btnB.action.WasPressedThisFrame())
            {
                // tabletNextSentence();
                yield return StartCoroutine(tabletSentence_Play());
                Debug.Log("B누름");
                break;              // 입력받으면 while문 탈출. 코루틴 종료
            }
            // print("idx = 3 while문 끝남");
            yield return null;      // 입력받을떄까지 대기
        }
        print("모든 코루틴이 끝남");
    }

    #endregion

    #region TutorialFlow

    IEnumerator tutorialSentence_Play()
    {
        while ((curIndex == 0) && (temp < delay))
        {
            Debug.Log("튜토리얼 시작");
            temp += Time.deltaTime;
            // 코루틴 시작 후 delay 후에 나타남
            if (temp >= delay)
            {
                // ShowText = m_tutorialTextData[curIndex];
                // npcText.text = ShowText;
                
                showUI.sprite = m_tutorialImgData[curIndex];
                isBtn1Pressed = false;
                isBtn2Pressed = false;
                L_Button[curIndex].SetActive(true);
                R_Button[curIndex].SetActive(true);
                break;
            }
            yield return null;
        }
        
        //엄지로 빛나는 버튼 A, X를 모두 눌러보세요.
        while (curIndex == 0)
        {
            Debug.Log("엄지로 빛나는 버튼 A, X를 모두 눌러보세요.");
            if (btnA.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("A 누름");
                R_Button[curIndex].SetActive(false);
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnX.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("X 누름");
                L_Button[curIndex].SetActive(false);
                isBtn2Pressed = true;
                temp = 0f;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    L_Button[curIndex].SetActive(true);
                    R_Button[curIndex].SetActive(true);
                    break;
                }
            }
            yield return null;
        }
        
        // 엄지로 빛나는 버튼 B, Y를 모두 눌러보세요.
        while (curIndex == 1)
        {
            // 두 버튼 한번씩은 눌러야 함.
            Debug.Log("엄지로 빛나는 버튼 B, Y를 모두 눌러보세요.");
            if (btnB.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("B 누름");
                R_Button[curIndex].SetActive(false);
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnY.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("Y 누름");
                L_Button[curIndex].SetActive(false);
                isBtn2Pressed = true;
                temp = 0f;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    L_Button[curIndex].SetActive(true);
                    R_Button[curIndex].SetActive(true);
                    break;
                }
            }
            yield return null;
        }
        
        // 엄지로 아날로그 스틱을 움직여보세요.
        while (curIndex == 2)
        {
            Debug.Log("엄지로 아날로그 스틱을 움직여보세요.");
            
            if (btnRThumbStick.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("btnRThumbStick 돌림");
                R_Button[curIndex].SetActive(false);
                isBtn2Pressed = true;
                temp = 0f;
            }
            if (btnLThumbStick.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("btnLThumbStick 돌림");
                L_Button[curIndex].SetActive(false);
                isBtn1Pressed = true;
                temp = 0f;
            }
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    L_Button[curIndex].SetActive(true);
                    R_Button[curIndex].SetActive(true);
                    break;
                }
            }
            yield return null;
        }
        
        // 이번엔 검지를 이용해 컨트롤러의 트리거를 쥐어보세요.
        while (curIndex == 3)
        {
            Debug.Log("이번엔 검지를 이용해 컨트롤러의 트리거를 쥐어보세요.");
            if (btnRTrigger.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("RTrigger 누름");
                R_Button[curIndex].SetActive(false);
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnLTrigger.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("LTrigger 누름");
                L_Button[curIndex].SetActive(false);
                isBtn2Pressed = true;
                temp = 0f;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    L_Button[curIndex].SetActive(true);
                    R_Button[curIndex].SetActive(true);
                    break;
                }
            }
            yield return null;
        }
        
        // 그립 버튼을 확인하고 중지로 쥐어보세요.
        while (curIndex == 4)
        {
            Debug.Log("그립 버튼을 확인하고 중지로 쥐어보세요.");
            if (btnRGrip.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("RGrip 누름");
                R_Button[curIndex].SetActive(false);
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnLGrip.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("LGrip 누름");
                L_Button[curIndex].SetActive(false);
                isBtn2Pressed = true;
                temp = 0f;
            }
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    break;
                }
            }
            yield return null;
        }
        
        // 이제 이 가상 손으로 무엇을 할 수 있는지 볼까요?
        while (curIndex == 5)
        {
            Debug.Log("이제 이 가상 손으로 무엇을 할 수 있는지 볼까요?");
            temp += Time.deltaTime;
            // 파티클 호출 함수 
            if (temp >= 5)
            {
                Debug.Log("손 바뀜");
                ChangeController();
                curIndex++;
                showUI.sprite = m_tutorialImgData[curIndex];
                temp = 0;
                break;
            }
            yield return null;
        }
        
        // 주먹을 움켜쥐려면 그립 버튼과 트리거 버튼을 모두 쥐고 있으세요.
        while (curIndex == 6)
        {
            Debug.Log("주먹을 움켜쥐려면 그립 버튼과 트리거 버튼을 모두 쥐고 있으세요.");
            // 둘다 쥐기
            if (btnRTrigger.action.IsPressed() && btnRGrip.action.IsPressed() && !isBtn1Pressed)
            {
                Debug.Log("오른쪽 주먹쥠");
                isBtn1Pressed = true;
            }
            if (btnLTrigger.action.IsPressed() && btnLGrip.action.IsPressed() && !isBtn2Pressed)
            {
                Debug.Log("왼쪽 주먹쥠");
                isBtn2Pressed = true;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    break;
                }
            }
            yield return null;
        }
        
        // 가리키려면 그립버튼을 쥔 상태에서 검지만 떼세요.
        while (curIndex == 7)
        {
            Debug.Log("가리키려면 그립버튼을 쥔 상태에서 검지만 떼세요.");
            
            if (!btnRTrigger.action.WasPressedThisFrame() && btnRGrip.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("오른쪽 가리킴");
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (!btnLTrigger.action.WasPressedThisFrame() && btnLGrip.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("왼쪽 가리킴");
                isBtn2Pressed = true;
                temp = 0f;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    temp = 0f;
                    vrButton.SetActive(true);
                    break;
                }
            }
            yield return null;
        }
        
        // 이제 검지를 이용해 앞에 있는 버튼을 눌러보세요
        while (curIndex == 8)
        {
            Debug.Log("이제 검지를 이용해 앞에 있는 버튼을 눌러보세요");
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isPressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    vrButton.SetActive(false);
                    SpawnCube();
                    temp = 0f;
                    break;
                }
            }
            yield return null;
        }
        
        // 물체를 집으려면 중지로 그립 버튼을 쥔 상태를 유지하면 됩니다.
        // 손을 뻗어서 블록 하나를 집어보세요.
        while (curIndex == 9)
        {
            for (int i = 0; i < grabbable.Count; i++)
            {
                grabbable[i].selectEntered.AddListener(OnGrab);        // 잡으면 isGrab = true로 바꾸는 함수 등록
            }
            // 여러 개 큐브중 적어도 하나를 잡으면 다음 인덱스로 넘어가기
            if (isGrab)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    temp = 0f;
                    break;
                }
            }
            yield return null;
        }
        
        // 놓으려면 그립 버튼을 때세요.
        while (curIndex == 10)
        {
            for (int i = 0; i < grabbable.Count; i++)
            {
                grabbable[i].selectExited.AddListener(OnRelease);        // 잡으면 isGrab = true로 바꾸는 함수 등록
            }
            // grabbable.selectExited.AddListener(OnRelease);        // 놓으면 isGrab = false로 바꾸는 함수 등록
            // 놓으면 다음 인덱스로 넘어가기
            if (isGrab == false)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    temp = 0f;
                    break;
                }
            }
            yield return null;
        }
        
        // 이동하려면 엄지로 아날로그 스틱을 움직여보세요.
        // 왼쪽은 이동, 오른쪽은 회전입니다.
        while (curIndex == 11)
        {
            Debug.Log("이동하려면 엄지로 아날로그 스틱을 움직여보세요.");
            SetComponentEnabled<ActionBasedContinuousMoveProvider>(true);
            SetComponentEnabled<ActionBasedContinuousTurnProvider>(true);
            if (btnLThumbStick.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("btnLThumbStick 돌림");
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnRThumbStick.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("btnRThumbStick 돌림");
                isBtn2Pressed = true;
                temp = 0f;
            }
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= 5)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    break;
                }
            }
            yield return null;
        }
        
        // 검지로 Trigger버튼을 쥐면 바닥에 표시된 흰색 원으로 빠르게 이동할 수 있습니다.
        while (curIndex == 12)
        {
            Debug.Log("검지로 Trigger버튼을 쥐면 바닥에 표시된 흰색 원으로 빠르게 이동할 수 있습니다.");
            SetComponentEnabled<TeleportationProvider>(true);
            SetComponentEnabled<ActivateTeleportationRay_Tuto>(true);
            
            if (btnRTrigger.action.WasPressedThisFrame() && !isBtn1Pressed)
            {
                Debug.Log("RTrigger 누름");
                isBtn1Pressed = true;
                temp = 0f;
            }
            if (btnLTrigger.action.WasPressedThisFrame() && !isBtn2Pressed)
            {
                Debug.Log("LTrigger 누름");
                isBtn2Pressed = true;
                temp = 0f;
            }
            
            // 한번 누르면 카운트를 세고 다음 UI뜨기까지 딜레이를 줌
            if (isBtn1Pressed && isBtn2Pressed)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    // 다음 UI띄우기
                    curIndex++;
                    showUI.sprite = m_tutorialImgData[curIndex];
                    isBtn1Pressed = false;
                    isBtn2Pressed = false;
                    portal.SetActive(true); // 포탈 활성화
                    tablet.SetActive(true); // 테블릿 활성화
                    break;
                }
            }
            yield return null;
        }
        
        // 이제 노란색 포탈로 이동해보겠습니다.  
        while (curIndex == tutorialUIIndex - 1)
        {
            // 테블릿을 잡았을 때
            XRGrabInteractable xrGrabInteractable = tablet.GetComponent<XRGrabInteractable>();
            xrGrabInteractable.selectEntered.AddListener(OnGrab);
            // 플레이어가 포탈 위에 올라갔을 때
            isPortal = playercontroller.onPortal;
            // 포탈에서 테블릿을 잡으면 다음 씬 
            if (isPortal && isGrab)
            {
                temp += Time.deltaTime;
                if (temp >= delay)
                {
                    portal.SetActive(false); // 포탈 소멸
                    curIndex = 0;
                    tabletNextSentence();
                    break;
                }
            }
            yield return null;
        }
    }

    #endregion

    #region TabletFlow

    IEnumerator tabletSentence_Play()
    {
        ChangeController();
        SetComponentEnabled<ActionBasedContinuousMoveProvider>(true);
        SetComponentEnabled<ActionBasedContinuousTurnProvider>(true);
        SetComponentEnabled<TeleportationProvider>(true);
        SetComponentEnabled<ActivateTeleportationRay_Tuto>(true);
        int idx = 0;
        while (idx < tabletUIIndex)
        {
            showUI.sprite = m_tabletImgData[idx];
            idx++;
            yield return new WaitForSeconds(4f);
        }
        // 다 끝나고 ToolTip 활성화
        foreach (var VARIABLE in toolTip)
        {
            VARIABLE.ActiveToolTip();
        }
        print("코루틴 끝");
    }

    #endregion
    

    
}
