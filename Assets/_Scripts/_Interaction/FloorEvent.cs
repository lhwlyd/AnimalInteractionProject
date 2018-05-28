using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEvent : MonoBehaviour {

    [SerializeField]
    BaseAnimal[] animals;

    private void Start()
    {
        animals[0] = GameObject.Find("chicken_high").GetComponent<BaseAnimal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Floor")) {

            Debug.Log("Laser touch the floor!");
            foreach (BaseAnimal ba in animals) {
                if (!ba.IsBusy()) {
                    ba.GetStateMachine().ChangeState(new SearchForFun(~0, ba, 100, "PlayThing", ba.GetAgent()));
                }
            }
        }
    }
}
