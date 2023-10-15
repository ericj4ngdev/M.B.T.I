using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 도전과제 관리
// 현재 게임 상태 관리

public class KHUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle3X3, obstacle5X5;
    [SerializeField]
    private GameObject gateLeft, gateRight;

    private Vector3 doorPositionLeft;
    private Vector3 doorPositionRight;
    private Vector3 targetPositionLeft;
    private Vector3 targetPositionRight;
    float distanceToMoveX = -0.27f;
    private float duration = 1.7f;

    [SerializeField]
    private GameObject robot3X3, robot5X5;

    [SerializeField]
    private GameObject stamp;

    [SerializeField]
    private Animator trophyAnim3X3, trophyAnim5X5;

    [SerializeField]
    private AudioSource doorSound;

    private bool isTutorial;

    private void Start()
    {
        trophyAnim3X3.enabled = false;
        trophyAnim5X5.enabled = true;
        doorPositionLeft = gateLeft.transform.localPosition;
        doorPositionRight = gateRight.transform.localPosition;
        targetPositionLeft = new Vector3(doorPositionLeft.x + distanceToMoveX, doorPositionLeft.y, doorPositionLeft.z);
        targetPositionRight = new Vector3(doorPositionRight.x - distanceToMoveX, doorPositionRight.y, doorPositionRight.z);
    }

    public void SetTutorial()
    {
        isTutorial = true;
        obstacle3X3.SetActive(true);
        obstacle5X5.SetActive(false);
        robot3X3.SetActive(true);
        robot5X5.SetActive(false);

        StartCoroutine(OpenGate());

    }

    public void setMainGame()
    {
        isTutorial = false;
        obstacle3X3.SetActive(false);
        obstacle5X5.SetActive(true);
        robot3X3.SetActive(false);
        robot5X5.SetActive(true);

        StartCoroutine(OpenGate());
    }

    public void Fail()
    {
        Debug.Log("다시시도해보세요.");
        StartCoroutine(CloseGate());
    }

    public void Success()
    {
        Debug.Log("축하합니다.");
        if (!isTutorial)
        {
            stamp.SetActive(true);
            trophyAnim5X5.enabled = true;
            trophyAnim5X5.Play("GetTrophy", -1, 0);
        }
        else
        {
            Debug.Log("튜토리얼 완료");
            trophyAnim3X3.enabled = true;
            trophyAnim3X3.Play("GetTrophy", -1, 0);

            ChallengeManager.GetInstance().CompleteKHUChallenge();
        }
        StartCoroutine(CloseGate());
    }

    public void TestSuccess()
    {
        ChallengeManager.GetInstance().CompleteKHUChallenge();
    }

    private IEnumerator OpenGate()
    {
        doorSound.Play();
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newXLeft = Mathf.Lerp(doorPositionLeft.x, targetPositionLeft.x, t);
            float newXRight = Mathf.Lerp(doorPositionRight.x, targetPositionRight.x, t);

            gateLeft.transform.localPosition = new Vector3(newXLeft, doorPositionLeft.y, doorPositionLeft.z);
            gateRight.transform.localPosition = new Vector3(newXRight, doorPositionRight.y, doorPositionRight.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gateLeft.transform.localPosition = targetPositionLeft;
        gateRight.transform.localPosition = targetPositionRight;

    }

    private IEnumerator CloseGate()
    {
        yield return new WaitForSeconds(1.5f);
        doorSound.Play();

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newXLeft = Mathf.Lerp(targetPositionLeft.x, doorPositionLeft.x, t);
            float newXRight = Mathf.Lerp(targetPositionRight.x, doorPositionRight.x, t);

            gateLeft.transform.localPosition = new Vector3(newXLeft, doorPositionLeft.y, doorPositionLeft.z);
            gateRight.transform.localPosition = new Vector3(newXRight, doorPositionRight.y, doorPositionRight.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gateLeft.transform.localPosition = doorPositionLeft;
        gateRight.transform.localPosition = doorPositionRight;

    }

}
