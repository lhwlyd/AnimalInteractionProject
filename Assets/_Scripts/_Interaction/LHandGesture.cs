using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHandGesture : MonoBehaviour, IHandGesture {

    public LeapGestures playerAnimalGesturesRef;

    public Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public bool IsGrabbing() {
        if (playerAnimalGesturesRef == null) return false;
        return playerAnimalGesturesRef.LeftHandGrabStrength() > .5f;
    }

    public bool IsPalmOpen()
    {
        throw new System.NotImplementedException();
    }
}
