using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHandGesture : MonoBehaviour, IHandGesture {

    public AnimalGestures playerAnimalGesturesRef;

    // Use this for initialization
    void Start () {
        playerAnimalGesturesRef = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimalGestures>();
    }

    void Update() {
        //Debug.Log(IsGrabbing());
    }

    public bool IsGrabbing() {
        if (playerAnimalGesturesRef == null) return false;
        return playerAnimalGesturesRef.LeftHandGrabStrength() > .5f;
    }

    public bool IsPinching() {
        return false; //TODO, impement pinching... if we need it
    }
}
