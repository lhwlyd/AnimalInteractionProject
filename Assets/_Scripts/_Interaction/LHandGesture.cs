using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHandGesture : MonoBehaviour, IHandGesture {

    public LeapGestures playerAnimalGesturesRef;

    public bool IsGrabbing() {
        if (playerAnimalGesturesRef == null) return false;
        return playerAnimalGesturesRef.LeftHandGrabStrength() > .5f;
    }

    public bool IsPinching() {
        return false; //TODO, impement pinching... if we need it
    }
}
