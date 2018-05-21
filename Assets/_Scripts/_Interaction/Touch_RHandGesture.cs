using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_RHandGesture : MonoBehaviour, IHandGesture {
    public bool IsGrabbing()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f;
    }

    public bool IsPinching()
    {
        throw new System.NotImplementedException();
    }
}
