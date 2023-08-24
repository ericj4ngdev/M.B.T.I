using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BehaviorButton : MonoBehaviour
{
    public UnityEvent<List<int>> behaviourFilledEvent;

    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private GameObject[] BtnTypeArr = new GameObject[4];

    private Vector2 currentAnchoredPosition = new Vector2(5, -55);

    private void Start()
    {
        if (behaviourFilledEvent == null)
        {
            behaviourFilledEvent = new UnityEvent<List<int>> ();
        }
    }

    private const int maxBehaviourCount = 10;
    private List<int> robotBehaviorArray = new List<int>();  // 로봇 행동 배열
    private Stack<GameObject> btnArray = new Stack<GameObject>(); // 버튼 스택

    private GameObject addedBtn;
    private int countBtn = 0;


    public void OnClickedGoButton()
    {
        if (countBtn < maxBehaviourCount)
        {
            robotBehaviorArray.Add(0);
            AddBtnToCanvas(0, currentAnchoredPosition);
            currentAnchoredPosition.x += 60;
            countBtn += 1;
        }

        if (countBtn == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedRightButton()
    {
        if (countBtn < maxBehaviourCount)
        {
            robotBehaviorArray.Add(1);
            AddBtnToCanvas(1, currentAnchoredPosition);
            currentAnchoredPosition.x += 60;
            countBtn += 1;
        }

        if (countBtn == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedLeftButton()
    {
        if (countBtn < maxBehaviourCount)
        {
            robotBehaviorArray.Add(2);
            AddBtnToCanvas(2, currentAnchoredPosition);
            currentAnchoredPosition.x += 60;
            countBtn += 1;
        }

        if (countBtn == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedJumpButton()
    {
        if (countBtn < maxBehaviourCount - 1)
        {
            robotBehaviorArray.Add(3);
            AddBtnToCanvas(3, currentAnchoredPosition);
            currentAnchoredPosition.x += 110;
            countBtn += 2;
        }

        if (countBtn == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedUndoButton()
    {
        if (btnArray.Count > 0 && countBtn < maxBehaviourCount)
        { 

            if (btnArray.Peek().name == "Idx_JUMP(Clone)")
            {
                Debug.Log("점프버튼");
                currentAnchoredPosition.x -= 110;
                countBtn -= 2;
            }
            else
            {
                Debug.Log("점프버튼 아님");
                currentAnchoredPosition.x -= 55;
                countBtn -= 1;
            }

            btnArray.Peek().SetActive(false);
            btnArray.Pop();
            robotBehaviorArray.RemoveAt(robotBehaviorArray.Count - 1);
        }
    }

    private void OnBehaviourBtnFilled()
    {
        // 버튼 비활성화 해야함
        behaviourFilledEvent.Invoke(robotBehaviorArray);
        
    }

    private void ResetBtn()
    {
        foreach(GameObject btns in btnArray)
        {
            btns.SetActive(false);
        }

        foreach (GameObject btnUI in BtnTypeArr)
        {
            btnUI.SetActive(true);
        }

        countBtn = 0;
        currentAnchoredPosition = new Vector2(5, -55);
    }


    private void AddBtnToCanvas(int type, Vector2 anchoredPosition)
    {
        // 버튼을 생성하고 캔버스에 추가
        GameObject newButton = Instantiate(BtnTypeArr[type], canvasTransform);
        btnArray.Push(newButton);
        RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = anchoredPosition;
    }

    public void btnReset()
    {
        ResetBtn();
        robotBehaviorArray.Clear();
        btnArray.Clear();
    }
}
