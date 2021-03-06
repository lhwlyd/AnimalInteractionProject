﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowl : MonoBehaviour {

    protected float waterPoints;

    public GameObject waterDrop;
    public Transform waterGenerationPoint;

    private List<ParticleCollisionEvent> collisionEvents;

    private void OnParticleCollision(GameObject other)
    {
        /*
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, this, collisionEvents);

        foreach ( ParticleCollisionEvent e in collisionEvents) {
            // Check for particle type if we have more types of particle effects later

            if(waterPoints < 100)
                this.waterPoints++;
        }
        */

        Destroy(other, 0.2f);

        UpdateWater();
    }

    private void Start()
    {
        waterPoints = 0;
    }

    private void UpdateWater()
    {
        waterPoints++;
        if (waterPoints > 100) {
            Debug.Log("water!");
            GameObject water = Instantiate(waterDrop, waterGenerationPoint.position, Quaternion.identity) as GameObject;
            water.transform.position = water.transform.position + new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f));
            waterPoints = 0;
        }

    }
}
