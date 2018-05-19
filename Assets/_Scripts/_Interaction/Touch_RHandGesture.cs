using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_RHandGesture : MonoBehaviour, IHandGesture {
    public bool IsGrabbing()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.6f && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.6f;
    }

    public bool IsPinching()
    {
        throw new System.NotImplementedException();
    }
}
