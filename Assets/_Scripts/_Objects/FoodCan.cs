using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCan : MonoBehaviour {

    private int foodLeft;
    public GameObject food;
    public Transform generatePoint;

    private float foodOutTimer;
    private float time;

    public bool Grabbed;

    public Vector3 origPos;
    public Quaternion origRotation;

    private void Start()
    {
        foodLeft = 50;
        foodOutTimer = 0.25f;
        origPos = transform.position;
        origRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        // Need to fix this
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            if (time >= foodOutTimer) {
                GameObject newFood = Instantiate(food, generatePoint );
                newFood.transform.position = newFood.transform.position + new Vector3(Random.Range(0,0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f));
                newFood.transform.SetParent(null);
                foodLeft--;
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
