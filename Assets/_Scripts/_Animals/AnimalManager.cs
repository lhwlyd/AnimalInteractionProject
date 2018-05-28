using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : MonoBehaviour {

    public List<BaseAnimal> animals;

    // Use this for initialization
    void Start () {
        animals = FindObjectsOfType<BaseAnimal>().ToList();
    }
	
    public void BroadcastEventToAnimals(string eventName)
    {
        foreach(var animal in animals)
        {
            animal.gameObject.SendMessage(eventName);
        }
    }

    public void BroadcastEventToAnimals<T>(string eventName, T arg) {
        foreach(var animal in animals)
        {
            animal.gameObject.SendMessage(eventName, arg);
        }
    }
	
}
