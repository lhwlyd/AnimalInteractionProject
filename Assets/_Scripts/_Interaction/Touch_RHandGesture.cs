using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_RHandGesture : MonoBehaviour, IHandGesture {

    Vector3 previousPos = new Vector3();

    private void Start()
    {
        previousPos = transform.position;
    }

    private void Update()
    {
        previousPos = transform.position;
    }

    public Vector3 GetVelocity()
    {
        var currPos = transform.position;
        var velocity = (currPos - previousPos) / Time.deltaTime;
        previousPos = currPos;
        return velocity;
    }

    public bool IsGrabbing()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f;
    }

    public bool IsPalmOpen()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.1f && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.1f;
    }
}
