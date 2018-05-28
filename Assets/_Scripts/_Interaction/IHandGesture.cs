using UnityEngine;

public interface IHandGesture {

    bool IsGrabbing();

    bool IsPalmOpen();

    Vector3 GetVelocity();

}
