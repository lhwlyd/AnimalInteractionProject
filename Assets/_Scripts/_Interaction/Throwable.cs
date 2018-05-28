using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Throwable : MonoBehaviour {

    public float velocityMultiplier = 5;
    public AnimalManager animalManager;

    public bool thrown = false;
    public bool hitFloor = false;

    private void Start()
    {
        animalManager = GameObject.Find("Director").GetComponent<AnimalManager>();
    }

    private void Update()
    {
        if (thrown && hitFloor && animalManager != null)
        {
            animalManager.BroadcastEventToAnimals("OnFetch", this);
        }
    }

    void OnRelease(Vector3 velocity)
    {
        Debug.Log("Animal should fetch the ball");
        Debug.Log(velocity.magnitude);

        var rigidbody = GetComponent<Rigidbody>();
        var collider = GetComponent<SphereCollider>();

        if (rigidbody != null) {
            rigidbody.isKinematic = false;
            rigidbody.velocity = velocityMultiplier * velocity;
        }

        if(collider != null) {
            collider.isTrigger = false;
        }

        thrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            hitFloor = true;
        }
    }
}
