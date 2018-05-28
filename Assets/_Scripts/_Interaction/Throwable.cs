using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Throwable : MonoBehaviour {

    public float velocityMultiplier = 5;
    public AnimalManager animalManager;
    private Rigidbody rigidBody;
    private SphereCollider sphereCollider;

    public bool thrown = false;
    public bool hitFloor = false;
    public bool fetched = false;

    private void Start()
    {
        animalManager = GameObject.Find("Director").GetComponent<AnimalManager>();

        rigidBody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (thrown && hitFloor && animalManager != null)
        {
            Debug.Log("should be here");
            animalManager.BroadcastEventToAnimals<Throwable>("OnFetch", this);
        } else if(fetched && animalManager != null)
        {
            animalManager.BroadcastEventToAnimals<Throwable>("OnReturnFetch", this);
        }
    }

    void OnRelease(Vector3 velocity)
    {
        Debug.Log("Animal should fetch the ball");
        Debug.Log(velocity.magnitude);

        rigidBody.isKinematic = false;
        rigidBody.velocity = velocityMultiplier * velocity;
        
        sphereCollider.isTrigger = false;
       
        thrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            hitFloor = true;
        }
    }

    public void Reset()
    {
        thrown = false;
        hitFloor = false;

        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;

        sphereCollider.isTrigger = true;

        fetched = true;
        
    }

    public void ReturnThrowable()
    {
        fetched = false;
    }
}
