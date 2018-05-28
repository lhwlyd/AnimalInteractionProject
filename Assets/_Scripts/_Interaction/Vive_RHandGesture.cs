using UnityEngine;

public class Vive_RHandGesture : MonoBehaviour, IHandGesture
{

    Vector3 previousPos = new Vector3();

    private void Start()
    {
        previousPos = transform.position;
    }

    private void Update()
    {
        previousPos = transform.position;
        Debug.Log("Right is grabbing: " + IsGrabbing());
        Debug.Log("Right palm open: " + IsPalmOpen());
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
        return SteamVR_Controller.Input(1).GetHairTriggerDown();
    }

    public bool IsPalmOpen()
    {
        return SteamVR_Controller.Input(1).GetHairTriggerUp();
    }
}
