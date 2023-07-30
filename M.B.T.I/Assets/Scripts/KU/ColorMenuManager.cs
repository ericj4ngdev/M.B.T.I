using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ColorMenuManager : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public float spawnDistance = 0.5f;

    public Vector3 offset;
    public GameObject menu;
    public InputActionProperty menuBtn;
    
    
    private GameObject XRPlayer;
    // Start is called before the first frame update
    void Start()
    {
        XRPlayer = FindObjectOfType<XROrigin>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuBtn.action.WasPressedThisFrame())
        {
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);
        }

        // menu.transform.position = leftHand.position + offset;
        // menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        // menu.transform.forward *= -1;
    }
}
