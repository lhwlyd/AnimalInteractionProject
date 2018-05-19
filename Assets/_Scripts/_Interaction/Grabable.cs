using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grabable : MonoBehaviour {

    bool readyToBeGrabbed = false;
    //bool beingGrabbed;
    public GameObject GrabbingHandRef;
    public GameObject LGrabbingHandRef;
    public GameObject RGrabbingHandRef;

    private void Start() {
        LGrabbingHandRef = GameObject.FindGameObjectWithTag("leftHand");
    }

    private void FixedUpdate()
    {
        // if there is a hand in the scene, and this object is ready to be grabbed...
        if (GrabbingHandRef != null && readyToBeGrabbed)
        {

            // if the hand is not disabled (within LoS of the Leap)...
            if (GrabbingHandRef.activeSelf == true)
            {

                var gesture = GetHandGesture(GrabbingHandRef);

                // if the gesture is not null (we did it right)...
                if (gesture != null)
                {
                    Debug.Log("WE IN HERE...");
                    if (gesture.IsGrabbing())
                    {
                        Debug.Log("still grabbing...");
                        Grabbed();
                    }
                    else
                    {
                        ReleaseGrabbing();
                    }
                }
            }
        }
        else if (!readyToBeGrabbed) ReleaseGrabbing();
    }

    private void Grabbed()
    {
        Debug.Log("Grabbing");
        transform.parent = GrabbingHandRef.transform;
    }

    private void ReleaseGrabbing()
    {
        //Debug.Log("RELEASING!!!!");
        //GrabbingHandRef = null;
        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        switch (other.tag)
        {
            case "leftHand":
                PrepareToGrab(other.gameObject);
                break;
        }

    }

    

    private void OnTriggerExit(Collider other){
        readyToBeGrabbed = false;
    }

    private void PrepareToGrab(GameObject handRef) {
        GrabbingHandRef = handRef;
        readyToBeGrabbed = true;
    }

    private IHandGesture GetHandGesture(GameObject HandRef)
    {
        var LHandGestureRef = HandRef.GetComponent<Touch_LHandGesture>();
        var RHandGestureRef = HandRef.GetComponent<Touch_RHandGesture>();
        Debug.Log(LHandGestureRef);
        Debug.Log(RHandGestureRef);

        if (LHandGestureRef != null) return LHandGestureRef;
        if (RHandGestureRef != null) return RHandGestureRef;
        else return null;
    }
}
