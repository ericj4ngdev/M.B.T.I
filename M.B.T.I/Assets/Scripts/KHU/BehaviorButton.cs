using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BehaviorButton : MonoBehaviour
{
    public UnityEvent<List<int>> behaviourFilledEvent;

    private const int btnDistance = 250;    

    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private GameObject[] BtnTypeArr = new GameObject[4];

    private Vector2 currentAnchoredPosition = new Vector2(78, -1045);

    private void Start()
    {
        if (behaviourFilledEvent == null)
        {
            behaviourFilledEvent = new UnityEvent<List<int>> ();
        }
    }

    private int maxBehaviourCount;
    private List<int> robotBehaviorArray = new List<int>();  // 로봇 행동 배열
    private Stack<GameObject> btnArray = new Stack<GameObject>(); // 버튼 스택

    private GameObject addedBtn;
    private int behaviourCount = 0;

    public void set3x3Obstacle()
    {
        maxBehaviourCount = 8;
    }

    public void set5x5Obstacle()
    {
        maxBehaviourCount = 11;
    }

    public void OnClickedGoButton()
    {
        if (behaviourCount < maxBehaviourCount)
        {
            robotBehaviorArray.Add(0);
            AddBtnToCanvas(0, currentAnchoredPosition);
            currentAnchoredPosition.x += btnDistance;
            behaviourCount += 1;
        }
        
        if (behaviourCount == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedRightButton()
    {
        if (behaviourCount < maxBehaviourCount)
        {
            robotBehaviorArray.Add(1);
            AddBtnToCanvas(1, currentAnchoredPosition);
            currentAnchoredPosition.x += btnDistance;
            behaviourCount += 1;
        }
        
        if (behaviourCount == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedLeftButton()
    {
        if (behaviourCount < maxBehaviourCount)
        {
            robotBehaviorArray.Add(2);
            AddBtnToCanvas(2, currentAnchoredPosition);
            currentAnchoredPosition.x += btnDistance;
            behaviourCount += 1;
        }

        if (behaviourCount == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedJumpButton()
    {
        if (behaviourCount < maxBehaviourCount - 1)
        {
            robotBehaviorArray.Add(3);
            AddBtnToCanvas(3, currentAnchoredPosition);
            currentAnchoredPosition.x += btnDistance * 2;
            behaviourCount += 2;
        }

        if (behaviourCount == maxBehaviourCount)
        {
            // 이벤트 발생
            OnBehaviourBtnFilled();
        }
    }

    public void OnClickedUndoButton()
    {
        if (btnArray.Count > 0 && behaviourCount < maxBehaviourCount)
        { 
            if (btnArray.Peek().name == "Idx_JUMP(Clone)")
            {
                Debug.Log("점프버튼");
                currentAnchoredPosition.x -= btnDistance * 2;
                behaviourCount -= 2;
            }
            else
            {
                Debug.Log("점프버튼 아님");
                currentAnchoredPosition.x -= btnDistance;
                behaviourCount -= 1;
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

        behaviourCount = 0;
        currentAnchoredPosition = new Vector2(78, -1045);
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
