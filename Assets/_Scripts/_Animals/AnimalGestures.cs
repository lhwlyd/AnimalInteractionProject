using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

/**
 * This script is responsible for capturing the raw Leap input, to be used
 * by other scripts in order to translate this data into appropriate gestures.
 */
public class AnimalGestures : MonoBehaviour
{

    private Controller controller;
    List<Hand> hands;

    private Hand leftHand;
    private Hand rightHand;

    private void Awake()
    {
        controller = new Controller();
        hands = new List<Hand>();
    }

    private void FixedUpdate()
    {
        Frame currFrame = controller.Frame(); //The latest frame
        Frame previous = controller.Frame(1); //The previous frame
        hands = currFrame.Hands;

        //Debug.Log(hands.Count);

        if (hands.Count == 1)
        {
            var hand0 = hands[0];
            if (hand0.IsLeft)
            {
                leftHand = hand0;
                rightHand = null;
                //Debug.Log("Left hand is only present");
            }
            else
            {
                leftHand = null;
                rightHand = hand0;
                //Debug.Log("Right hand is only present");
            }
        }
        else if (hands.Count > 1)
        {
            var hand0 = hands[0];
            var hand1 = hands[1];

            if (hand0.IsLeft)
            {
                //Debug.Log("correct order both hands");
                leftHand = hand0;
                rightHand = hand1;
            }
            else
            {
                //Debug.Log("Incorrect order both hands");
                leftHand = hand1;
                rightHand = hand0;
            }
        }
        else if (hands.Count < 1)
        {
            //Debug.Log("No hands are present");
            leftHand = null;
            rightHand = null;
        }
    }

    public float LeftHandGrabStrength()
    {
        return leftHand == null ? 0.0f : leftHand.GrabStrength;
    }

    public float RightHandGrabStrength()
    {
        return rightHand == null ? 0.0f : rightHand.GrabStrength;
    }
}
