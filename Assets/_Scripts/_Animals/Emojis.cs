using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emojis : MonoBehaviour {

    BaseAnimal animal;
    public GameObject angryFace;
    public GameObject smileFace;
    public GameObject loveFace;

    private float timer;

    float lastIntimateLevel;


	// Use this for initialization
	void Start () {
        animal = this.GetComponent<BaseAnimal>();
        lastIntimateLevel = animal.GetIntimateLevel();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(lastIntimateLevel);
        timer += Time.deltaTime;
        float curIntimateLevel = animal.GetIntimateLevel();
        if (curIntimateLevel - lastIntimateLevel >= 10)
        {
            lastIntimateLevel = curIntimateLevel;
            Instantiate(smileFace);
        }
        else if (lastIntimateLevel - curIntimateLevel >= 10) {
            lastIntimateLevel = curIntimateLevel;
            Instantiate(angryFace);
        } else if (curIntimateLevel == 100 && lastIntimateLevel == 100 && timer > 10f) {
            timer = 0f;
            Instantiate(loveFace);
        }

    }
}
