using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petting : IState {

    GameObject HeartParticleObject = null;

	public Petting(Transform parent){
        foreach (Transform transform in parent) {
            if (transform.CompareTag("particle_hearts")) {
                HeartParticleObject = transform.gameObject;
                break;
            }
        }
	}
    public void Enter()
    {
        if (HeartParticleObject != null) {
            HeartParticleObject.SetActive(true);
        }
    }

    public void Execute()
    {
        Debug.Log("being petted");
    }

    public void Exit()
    {
        if (HeartParticleObject != null)
        {
            HeartParticleObject.SetActive(false);
        }
    }
}
