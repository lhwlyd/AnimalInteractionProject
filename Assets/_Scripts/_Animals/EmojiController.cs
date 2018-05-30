using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour {

    GameObject animal;
    GameObject player;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
        animal = GameObject.Find("chicken_high");
        player = GameObject.Find("Camera (eye)");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(animal.transform.position.x, animal.transform.position.y + 1, animal.transform.position.z );
        this.transform.LookAt(player.transform);
    }
}
