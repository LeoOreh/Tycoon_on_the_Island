using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_UI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("Land_1_stones");
    }
}
