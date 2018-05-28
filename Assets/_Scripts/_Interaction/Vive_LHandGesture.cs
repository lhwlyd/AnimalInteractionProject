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
        return SteamVR_Controller.Input(2).GetHairTrigger();
    }

    public bool IsPalmOpen()
    {
        return !SteamVR_Controller.Input(2).GetHairTrigger();
    }
}
