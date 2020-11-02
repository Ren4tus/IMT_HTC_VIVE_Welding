using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject cam;
    public int xAngle;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Transform rawCameraAngle = cam.transform;
        Vector3 roateValue = new Vector3(0, 0, 0);
        roateValue.x = rawCameraAngle.rotation.x+(rawCameraAngle.rotation.x-xAngle);
        rawCameraAngle.Rotate(roateValue);
        transform.LookAt(rawCameraAngle.transform);

        

    }
}
