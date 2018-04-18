﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grabable : MonoBehaviour {

    bool readyToBeGrabbed;
    bool beingGrabbed;
    Collider grabbingHand;
    AnimalGestures animalGesturesRef;

    NavMeshAgent agent;
    [SerializeField]
    GameObject playerRef;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2.0f;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animalGesturesRef = playerRef.GetComponent<AnimalGestures>();
    }

    private void Update()
    {

        // Grabbed?
        if (readyToBeGrabbed)
        {
            if (animalGesturesRef.LeftHandGrabStrength() > 0.5f)
            {
                Grabbed();
            }
            else
            {
                if (beingGrabbed && animalGesturesRef.LeftHandGrabStrength() < 0.3f)
                {
                    ReleaseGrabbing();
                }
            }
        }
    }

    private void Grabbed()
    {
        if ( agent != null ) {
            GetComponent<BaseAnimal>().RecordAgentState(ref agent);
        }
        transform.parent = grabbingHand.transform;

        agent.velocity = Vector3.zero;
        agent.ResetPath();
        beingGrabbed = true;
    }

    private void ReleaseGrabbing()
    {
        grabbingHand = null;
        transform.parent = null;

        //GetComponent<NavMeshAgent>().enabled = true;
        readyToBeGrabbed = false;
        beingGrabbed = false;

        if (agent != null) {
            GetComponent<BaseAnimal>().RestoreAgentState(ref agent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "leftHand":
                grabbingHand = other;
                Debug.Log("Grabbing with left hand");
                if (animalGesturesRef.LeftHandGrabStrength() < 0.3f)
                {
                    readyToBeGrabbed = true;
                }
                else
                {

                }
                break;

            case "rightHand":
                grabbingHand = other;

                Debug.Log("Grabbing with right hand");
                if (animalGesturesRef.RightHandGrabStrength() < 0.3f)
                {
                    readyToBeGrabbed = true;
                }
                else
                {
                }

                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        switch (other.tag)
        {
            case "leftHand":
            case "rightHand":
                ReleaseGrabbing();
                break;
        }

    }
}
