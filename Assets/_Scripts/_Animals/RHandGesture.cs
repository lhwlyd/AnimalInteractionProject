using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandGesture : MonoBehaviour, IHandGesture {
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsGrabbing()
    {
        throw new System.NotImplementedException();
    }

    public bool IsPinching()
    {
        throw new System.NotImplementedException();
    }
}
