using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 열기구 올라타기
public class GetOnAirBalloon : MonoBehaviour
{
    [SerializeField] private GameObject airballoon;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameMenu;

    public void OnClickAirballoon()     // 태블릿의 열기구 버튼을 누를 때
    {
        player.transform.position = airballoon.transform.position;
        transform.SetParent(airballoon.transform);
        gameMenu.transform.SetParent(airballoon.transform);        
    }

    public void GetOff()
    {
        Debug.Log("내림");
        transform.SetParent(null);
        gameMenu.transform.SetParent(null);
    }
}
