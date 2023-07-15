using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Message")]
    [TextArea]
    public string[] m_wellcomeTextData;
    [TextArea]
    public string[] m_tutorialTextData;
    [TextArea]
    public string[] m_tabletTextData;
    
    [Header("Debug")]
    public int wellcomeTextIndex;
    public int tutorialTextIndex;
    public int tabletTextIndex;
    public int curIndex;
    public string ShowText;
    public TextMeshProUGUI npcText;
    public float delay = 5f;
    public float temp = 0;
    public bool isEnd;
    public bool isDialog;
    
    [Header("Key")]
    public InputActionProperty btnA;
    public InputActionProperty btnB;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        curIndex = 0;
        wellcomeTextIndex = m_wellcomeTextData.Length;
        tutorialTextIndex = m_tutorialTextData.Length;
        tabletTextIndex = m_tabletTextData.Length;
        temp = 0;
        ShowText = m_wellcomeTextData[curIndex];
        npcText.text = ShowText;
        isEnd = false;
        isDialog = false;
        StartCoroutine(WellcomeSentence_Play());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialog == false)
        {
            tutorialNextSentence();
        }

        // 튜토리얼 마지막에 조건
        // 나중에는 OntriggerEnter 해서 isEnd = true로 만들 예정
        if (Input.GetKeyDown(KeyCode.A))
        {
            isEnd = true;
        }
    }

    IEnumerator WellcomeSentence_Play()
    {
        int idx = 0;
        while (idx < wellcomeTextIndex)
        {
            ShowText = m_wellcomeTextData[idx];
            npcText.text = ShowText;
            idx++;
            isDialog = false;
            yield return new WaitForSeconds(3f);
        }

        while (idx == wellcomeTextIndex)
        {
            // if(Input.GetKeyDown(KeyCode.Keypad1))
            if (btnA.action.WasPressedThisFrame())
            {
                tutorialNextSentence();
                break;
            }
            // if(Input.GetKeyDown(KeyCode.Keypad2))
            if (btnB.action.WasPressedThisFrame())
            {
                tabletNextSentence();
                break;              // 입력받으면 while문 탈출. 코루틴 종료
            }
            // print("idx = 3 while문 끝남");
            yield return null;      // 입력받을떄까지 대기
        }
        print("모든 코루틴이 끝남");
    }

    void tutorialNextSentence()
    {
        isDialog = true;
        StartCoroutine(tutorialSentence_Play());
    }

    void tabletNextSentence()
    {
        isDialog = true;
        StartCoroutine(tabletSentence_Play());
    }
    
    IEnumerator tutorialSentence_Play()
    {
        while (curIndex < tutorialTextIndex)
        {
            temp += Time.deltaTime;
            if (temp >= delay)
            {
                ShowText = m_tutorialTextData[curIndex];
                npcText.text = ShowText;
                curIndex++;
                temp = 0f;
                isDialog = false;
                Debug.Log("다음 문장");
                break;
            }
            yield return null;
        }
        while (curIndex == 0)
        {
            if (btnA.action.WasPressedThisFrame())
            {
                curIndex++;
                break;
            }
            yield return null;
        }
        while (curIndex == 1)
        {
            if (btnB.action.WasPressedThisFrame())
            {
                curIndex++;
                break;
            }
            yield return null;
        }
        while (curIndex == tutorialTextIndex)
        {
            // 포탈로 이동시 isEnd변수가 true가 되어
            // 타블렛 코루틴으로 이동. break되어 코루틴 종료
            if (isEnd)
            {
                curIndex = 0;
                tabletNextSentence();
                break;
            }
            yield return null;
        }
    }

    IEnumerator tabletSentence_Play()
    {
        while (curIndex < tabletTextIndex)
        {
            temp += Time.deltaTime;
            if (temp >= delay)
            {
                ShowText = m_tabletTextData[curIndex];
                npcText.text = ShowText;
                curIndex++;
                isDialog = false;
                temp = 0f;
                Debug.Log("다음 문장");
                break;
            }
            yield return null;
        }
        print("코루틴 끝");
    }


}
