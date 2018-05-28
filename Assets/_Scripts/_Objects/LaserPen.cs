using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaserPen : MonoBehaviour {

    public GameObject laser;
    public float offset = 0.1f;

    private GameObject laserDot;

    public LineRenderer laserLine;

    public List<BaseAnimal> animals;

	// Use this for initialization
	void Start () {
        animals = FindObjectsOfType<BaseAnimal>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        { // all layers except the second layer(ignore ray cast)
            
            if (laserDot == null)
            {
                laserDot = Instantiate(laser, hit.point + hit.normal * offset, Quaternion.identity) as GameObject;
            }
            else
            {
                laserDot.SetActive(true);
                laserDot.transform.position = hit.point + hit.normal * offset;
            }

            if (laserLine != null)
            {
                laserLine.gameObject.SetActive(true);
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit.point);
            }

            gameObject.SendMessage("Test");
        }
        else {
            laserLine.gameObject.SetActive(false);
            if (laserDot != null) laserDot.SetActive(false);
        }

    }
}
