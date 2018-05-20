using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_LHandGesture : MonoBehaviour, IHandGesture
{
    public bool IsGrabbing()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f;
    }

    public bool IsPinching()
    {
        throw new System.NotImplementedException();
    }
}
