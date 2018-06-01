using UnityEngine;

public class Vive_LHandGesture : MonoBehaviour, IHandGesture {

    Vector3 previousPos = new Vector3();

    private void Start()
    {
        previousPos = transform.position;
    }

    private void Update()
    {
        previousPos = transform.position;
        Debug.Log("Grabbing: " + IsGrabbing());
        Debug.Log("Palm open: " + IsPalmOpen());
    }

    public Vector3 GetVelocity()
    {
        var currPos = transform.position;
        var velocity = (currPos - previousPos) / Time.deltaTime;
        previousPos = currPos;
        return velocity;
    }

    public bool IsGrabbing()
    {
        return SteamVR_Controller.Input(1).GetHairTrigger();
    }

    public bool IsPalmOpen()
    {
        return !SteamVR_Controller.Input(1).GetHairTrigger();
    }
}
