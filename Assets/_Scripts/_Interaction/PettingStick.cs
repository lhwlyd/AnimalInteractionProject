using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PettingStick : MonoBehaviour, IHandGesture {
    public Vector3 GetVelocity()
    {
        return Vector3.zero;
    }

    public bool IsGrabbing()
    {
        return false;
    }

    public bool IsPalmOpen()
    {
        return true;
    }
}
