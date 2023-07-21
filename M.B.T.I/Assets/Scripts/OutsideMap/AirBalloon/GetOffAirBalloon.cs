using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOffAirBalloon : MonoBehaviour
{
    [SerializeField] private AirballoonAnimation airballoonAnim;
    [SerializeField] private GameObject player;

    public void Awake()
    {
        // 이벤트 Subscriber 추가
        airballoonAnim.OnBalloonArrive += GetOffwithDelay;
    }

    // 내림 버튼을 눌렀을 때 호출할 함수
    public void OnClickExit()
    {
        airballoonAnim.isPlayerWantToGetOff = true;
    }

    private void GetOffwithDelay()
    {
        Invoke("GetOff", 0.5f);
    }

    private void GetOff()
    {
        Debug.Log("내림");
        transform.SetParent(null);
        // 열기구 내리기
        Vector3 currentPosition = player.transform.position;

        currentPosition.y = 5f;

        player.transform.position = currentPosition;
        airballoonAnim.isPlayerWantToGetOff = false;
    }
}
