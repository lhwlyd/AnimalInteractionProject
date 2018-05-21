using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grabable : MonoBehaviour
{

    public bool readyToBeGrabbed = false;
    //bool beingGrabbed;
    public GameObject GrabbingHandRef;
    public GameObject LGrabbingHandRef;
    public GameObject RGrabbingHandRef;

    public BaseAnimal animalComponentRef;
    public bool grabbed = false;

    private void Start()
    {
        LGrabbingHandRef = GameObject.FindGameObjectWithTag("leftHand");
        RGrabbingHandRef = GameObject.FindGameObjectWithTag("rightHand");
        animalComponentRef = GetComponent<BaseAnimal>();
    }

    private void FixedUpdate()
    {
        // if there is a hand in the scene, and this object is ready to be grabbed...
        if (readyToBeGrabbed)
        {

            // if the hand is not disabled (within LoS of the Leap)...
            if (GrabbingHandRef.activeSelf == true)
            {

                var gesture = GetHandGesture(GrabbingHandRef);

                // if the gesture is not null (we did it right)...
                if (gesture != null)
                {
                    bool grabbing = gesture.IsGrabbing();
                    // Debug.Log(grabbing);
                    if (gesture.IsGrabbing())
                    {
                        // Debug.Log("still grabbing...");
                        Grabbed();
                    }
                    // else
                    // {
                    //     ReleaseGrabbing();
                    // }
                }
            }
        }


        if (GrabbingHandRef != null && grabbed)
        {
            // if the hand is not disabled (within LoS of the Leap)...
            if (GrabbingHandRef.activeSelf == true)
            {

                var gesture = GetHandGesture(GrabbingHandRef);

                // if the gesture is not null (we did it right)...
                if (gesture != null)
                {
                    bool grabbing = gesture.IsGrabbing();
                    // Debug.Log(grabbing);
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
        // else if (!readyToBeGrabbed) ReleaseGrabbing();
    }

    private void Grabbed()
    {
        // Debug.Log("Grabbing");
        transform.parent = GrabbingHandRef.transform;
        if(animalComponentRef != null && !grabbed) {
            animalComponentRef.SetBusy(BaseAnimal.BusyType.Grabbed);
        }
        grabbed = true;
    }

    private void ReleaseGrabbing()
    {
        Debug.Log("RELEASING!!!");
        transform.parent = null;
        grabbed = false;
        if(animalComponentRef != null) {
            animalComponentRef.ExitBusy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!grabbed)
        {
            switch (other.tag)
            {
                case "leftHand":
                    PrepareToGrab(other.gameObject);
                    break;
                case "rightHand":
                    PrepareToGrab(other.gameObject);
                    break;
            }
        }

    }



    private void OnTriggerExit(Collider other)
    {
        // GrabbingHandRef = null;
        readyToBeGrabbed = false;
    }

    private void PrepareToGrab(GameObject handRef)
    {
        GrabbingHandRef = handRef;
        readyToBeGrabbed = true;
    }

    private IHandGesture GetHandGesture(GameObject HandRef)
    {
        var LHandGestureRef = HandRef.GetComponent<Touch_LHandGesture>();
        var RHandGestureRef = HandRef.GetComponent<Touch_RHandGesture>();

        if (LHandGestureRef != null) return LHandGestureRef;
        if (RHandGestureRef != null) return RHandGestureRef;
        else return null;
    }
}
