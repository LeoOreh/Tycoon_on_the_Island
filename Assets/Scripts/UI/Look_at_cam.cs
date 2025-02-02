using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_at_cam : MonoBehaviour
{
    Vector3 v = new Vector3 (0, 1, 1);
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cam, v);
    }
}
