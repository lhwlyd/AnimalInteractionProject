using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCan : MonoBehaviour {

    private int foodLeft;
    public GameObject food;
    public Transform generatePoint;

    private float foodOutTimer;
    private float time;

    private void Start()
    {
        foodLeft = 50;
        foodOutTimer = 0.25f;
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        // Need to fix this
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            if (time >= foodOutTimer) {
                GameObject newFood = Instantiate(food, generatePoint);
                newFood.transform.SetParent(null);
                foodLeft--;
                time = 0;
            }
            
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
