using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    public UnityEvent connectToServerEvent;
    public UnityEvent MoveToRoomEvent;


    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            connectToServerEvent.Invoke();
            MoveToRoomEvent.Invoke();
            // StartCoroutine(DelayedInitializeRoom());
        }
    }

    private IEnumerator DelayedInitializeRoom()
    {
        connectToServerEvent.Invoke();
        yield return new WaitForSeconds(1.0f); // 딜레이 시간 (1초) 설정
        MoveToRoomEvent.Invoke();
    }
}
