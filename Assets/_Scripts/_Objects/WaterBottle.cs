﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottle : MonoBehaviour {

    private int waterLeft;
    public GameObject water;
    public Transform generatePoint;

    private float waterOutTimer;
    private float time;

    public bool Grabbed;

    public Vector3 origPos;
    public Quaternion origRotation;

    private void Start()
    {
        waterLeft = 10;
        waterOutTimer = 0.1f;
        origPos = transform.position;
        origRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        // Need to fix this
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            if (time >= waterOutTimer) {
                GameObject newWater = Instantiate(water, generatePoint);
                newWater.transform.SetParent(null);
                waterLeft--;
                time = 0;
            }
            
        }
	}

    void OnGrab()
    {
        Grabbed = true;
    }

    void OnRelease()
    {
        Grabbed = false;
        transform.position = origPos;
        transform.rotation = origRotation;
    }
}
