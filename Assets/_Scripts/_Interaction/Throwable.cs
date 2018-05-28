using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public float velocityMultiplier = 5;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("THROWABLE HIT SOMETHING");
    }
}
