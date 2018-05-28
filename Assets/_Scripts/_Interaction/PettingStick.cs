using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PettingStick : MonoBehaviour, IHandGesture {

    public Vector3 previousPos = new Vector3();

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
        return false;
    }

    public bool IsPalmOpen()
    {
        return true;
    }
}
