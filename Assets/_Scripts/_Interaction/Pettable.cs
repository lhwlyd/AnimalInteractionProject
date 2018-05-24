using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pettable : MonoBehaviour
{

    public GameObject LGrabbingHandRef;
    public GameObject RGrabbingHandRef;
    public BaseAnimal animalComponentRef;

    public bool petting = false;

    // Use this for initialization
    void Start()
    {
        LGrabbingHandRef = GameObject.FindGameObjectWithTag("leftHand");
        RGrabbingHandRef = GameObject.FindGameObjectWithTag("rightHand");
        animalComponentRef = GetComponent<BaseAnimal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!petting)
        {
            switch (other.tag)
            {
                case "leftHand":
                    Pet();
                    break;
                case "rightHand":
                    Pet();
                    break;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        StopPetting();
    }

    private void Pet() {
        petting = true;
        if (animalComponentRef != null) {
            animalComponentRef.SetBusy(BaseAnimal.BusyType.Petting);
        }
    }
    private void StopPetting() {
        petting = false;
        if (animalComponentRef != null)
        {
            animalComponentRef.ExitBusy();
        }
    }
}
