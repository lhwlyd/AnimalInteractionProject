using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {

    BaseAnimal animal;
	// Use this for initialization
	void Start () {
        animal = GameObject.Find("chicken_high").GetComponent<BaseAnimal>();
	}

    private void Update()
    {
        if (Input.GetKeyDown("space")) {
            // sleep
            if ( animal.GetHungerLevel() > 50f && animal.GetThirstLevel() > 50f ) {
                animal.UpdateHungerLevel(-40f);
                animal.UpdateThirstLevel(-40f);
                animal.gameObject.transform.localScale *= 1.5f;
            }
        }
    }
}
