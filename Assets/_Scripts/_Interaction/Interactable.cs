using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public GameObject LGrabbingHandRef;
    public GameObject RGrabbingHandRef;

    public GameObject PettingObjectRef;
    public IHandGesture Gesture;
    public BaseAnimal animalComponentRef;
    public GameObject HeartParticleObject = null;
    public GameObject InjureParticleObject = null;
    public bool readyToInteract = false;

    public float petTime = 1.0f;
    public float PET_MAX_TIME = 1.0f;
    public float petCutoff = 1.0f;

    public float injureTime = 1.0f;
    public float INJURE_MAX_TIME = 1.0f;
    public float injureCutoff = 1.0f;

    public Vector3 previousPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        LGrabbingHandRef = GameObject.FindGameObjectWithTag("leftHand");
        RGrabbingHandRef = GameObject.FindGameObjectWithTag("rightHand");
        animalComponentRef = GetComponent<BaseAnimal>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("particle_hearts"))
            {
                HeartParticleObject = child.gameObject;
                break;
            }
        }

        foreach (Transform child in transform)
        {
            if (child.CompareTag("particle_injure"))
            {
                InjureParticleObject = child.gameObject;
                break;
            }
        }
    }

    private void Update()
    {
        DetectInteraction();
        UpdatePetting();
        UpdateInjuring();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!readyToInteract)
        {
            switch (other.tag)
            {
                case "leftHand":
                    Interact(other.gameObject);
                    break;
                case "rightHand":
                    Interact(other.gameObject);
                    break;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        StopInteracting();
    }

    private void Interact(GameObject pettingObject) {
        readyToInteract = true;
        PettingObjectRef = pettingObject;
        Gesture = PettingObjectRef.GetComponent<IHandGesture>();
        previousPos = pettingObject.transform.position;
    }

    private void DetectInteraction() {
        if (animalComponentRef == null) return;

        if (PettingObjectRef == null) return;

        var currPos = PettingObjectRef.transform.position;

        var velocity = (currPos - previousPos) / Time.deltaTime;

        previousPos = currPos;

        var pettingVector = transform.forward;
        var petted = Vector3.Angle(velocity, transform.forward) >= 140;
        var mag = Vector3.Magnitude(velocity);
        //Debug.Log(mag);
        if(!OnPet(petted, mag))
            OnInjure(mag);

    }
    private void StopInteracting() {
        readyToInteract = false;
    }

    private bool OnPet(bool petted, float mag) {
        if (mag > petCutoff) return false;
        if (HeartParticleObject == null) return false;
        if (!readyToInteract) return false;
        if (Gesture == null) return false;
        if (!Gesture.IsPalmOpen()) return false;
        if (petted) {
            CancelInjuring();
            petTime = 0.0f;
            HeartParticleObject.SetActive(true);
            animalComponentRef.UpdateAggresionLevel(-0.025f);
            animalComponentRef.UpdateIntimateLevel(0.025f);
            return true;
        }
        return false;
    }

    private bool OnInjure(float mag) {
        if (InjureParticleObject == null) return false;
        if (!readyToInteract) return false;
        if (mag > injureCutoff) {
            CancelPetting();
            injureTime = 0.0f;
            InjureParticleObject.SetActive(true);
            animalComponentRef.UpdateAggresionLevel(0.025f);
            animalComponentRef.UpdateIntimateLevel(-0.025f);
            return true;
        }
        return false;
    }

    private void UpdatePetting() {
        if (HeartParticleObject == null) return;
        if (petTime < PET_MAX_TIME)
        {
            petTime += Time.deltaTime;
            if (petTime >= PET_MAX_TIME)
            {
                CancelPetting();
            }
        }
    }

    private void UpdateInjuring()
    {
        if (InjureParticleObject == null) return;
        if(injureTime < INJURE_MAX_TIME)
        {
            injureTime += Time.deltaTime;
            if(injureTime >= INJURE_MAX_TIME)
            {
                CancelInjuring();
            }
        }
    }

    private void CancelPetting()
    {
        if (HeartParticleObject == null) return;
        petTime = PET_MAX_TIME;
        HeartParticleObject.SetActive(false);
    }

    private void CancelInjuring()
    {
        if (InjureParticleObject == null) return;
        injureTime = INJURE_MAX_TIME;
        InjureParticleObject.SetActive(false);
    }
}
