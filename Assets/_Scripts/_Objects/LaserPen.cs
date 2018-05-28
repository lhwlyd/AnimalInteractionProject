using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPen : MonoBehaviour {

    public GameObject laser;
    public float offset = 0.1f;

    private GameObject laserInstance;

    public LineRenderer laserLine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        { // all layers except the second layer(ignore ray cast)
            Debug.Log(hit.point.magnitude);
            //Debug.Log(hit.point);
            //if (hit.point.magnitude == 0) {
            //    laserLine.gameObject.SetActive(false);
            //    return;
            //}
            if (laserInstance == null)
            {
                laserInstance = Instantiate(laser, hit.point + hit.normal * offset, Quaternion.identity) as GameObject;
            }
            else
            {
                laserInstance.transform.position = hit.point + hit.normal * offset;
            }

            if (laserLine != null)
            {
                laserLine.gameObject.SetActive(true);
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit.point);
            }
            
        }
        else {
            Debug.Log("NOT HITTING ANYTHING");
            laserLine.gameObject.SetActive(false);
            if(laserInstance) Destroy(laserInstance.gameObject);
        }

    }
}
