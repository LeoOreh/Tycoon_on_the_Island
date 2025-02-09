using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Camera_POST : MonoBehaviour
{
    void Start()
    {
        if (Settings.slider_quality > 0.95f) 
        { 
            GetComponent<PostProcessLayer>().enabled = true;
            GetComponent<PostProcessVolume>().enabled = true;
        }
        else
        {
            GetComponent<PostProcessLayer>().enabled = false;
            GetComponent<PostProcessVolume>().enabled = false;
        }
    }
}
