using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    TextMeshProUGUI txt;
    float time;
    int count;
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        count++;

        if(Time.time > time) 
        { 
            time++;
            txt.text = "FPS: " + count;
            count = 0; 
        }
    }
}
