using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCan : MonoBehaviour {

    private int foodLeft;
    public GameObject food;

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
        if (this.transform.rotation.z < -90f && this.transform.rotation.z > -270f)
        {
            if (time >= foodOutTimer) {
                Instantiate(food, transform);
                foodLeft--;
                time = 0;
            }
            
        }
	}
}
