using UnityEngine;

public class LaserPen : MonoBehaviour {

    public GameObject laser;
    public float offset = 0.1f;

    private GameObject laserDot;

    public LineRenderer laserLine;

    public AnimalManager animalManager;

    public bool Grabbed;

    public Vector3 origPos;
    public Quaternion origRotation;

    void Start() {
        animalManager = GameObject.Find("Director").GetComponent<AnimalManager>();
        origPos = transform.position;
        origRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit) && Grabbed)
        { // all layers except the second layer(ignore ray cast)
            
            if (laserDot == null)
            {
                laserDot = Instantiate(laser, hit.point + hit.normal * offset, Quaternion.identity) as GameObject;
            }
            else
            {
                laserDot.SetActive(true);
                laserDot.transform.position = hit.point + hit.normal * offset;
            }

            if (laserLine != null)
            {
                laserLine.gameObject.SetActive(true);
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit.point);
            }

            if(animalManager != null && hit.transform.CompareTag("Floor"))
                animalManager.BroadcastEventToAnimals<Vector3>("OnLaser", hit.point);
        }

        if(!Grabbed) {
            laserLine.gameObject.SetActive(false);
            if (laserDot != null) laserDot.SetActive(false);
        }

    }

    void OnGrab() {
        Grabbed = true;
    }

    void OnRelease() {
        Grabbed = false;
        transform.position = origPos;
        transform.rotation = origRotation;
    }
}
