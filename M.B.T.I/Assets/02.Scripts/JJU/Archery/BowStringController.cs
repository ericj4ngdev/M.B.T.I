using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;
    
    private XRGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField]
    private float bowStringStretchLimit = 0.0055f;

    private Transform interactor;

    private float strength;

    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    public void ResetBowString()
    {
        Debug.Log("ResetBowString");
        OnBowReleased?.Invoke(strength);
        strength = 0;
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    public void PrepareBowString()
    {
        interactor = midPointGrabObject.transform;
        OnBowPulled?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactor != null)
        {
            Vector3 midPointLocalSpace =
                midPointParent.InverseTransformPoint(midPointGrabObject.position);

            float midPointLocalXAbs = Mathf.Abs(midPointLocalSpace.x);

            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackToLimit(midPointLocalXAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalXAbs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.transform.position);
        }
    }

    private void HandlePullingString(float midPointLocalXAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.x > 0 && midPointLocalXAbs < bowStringStretchLimit)
        {
            strength = Remap(midPointLocalXAbs, 0, bowStringStretchLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(midPointLocalSpace.x, 0, 0);
        }    
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void HandleStringPulledBackToLimit(float midPointLocalXAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.x > 0 && midPointLocalXAbs > bowStringStretchLimit)
        {
            strength = 1;
            midPointVisualObject.localPosition = new Vector3(bowStringStretchLimit, 0, 0);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.x <= 0)
        {
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
