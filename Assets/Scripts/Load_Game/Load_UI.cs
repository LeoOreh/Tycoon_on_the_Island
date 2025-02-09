using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_UI : MonoBehaviour
{
    public GameObject image_load;
    public Image fill;

    public void Play()
    {
        DontDestroyOnLoad(gameObject);

        image_load.SetActive(true);
        StartCoroutine(Load());
    }


    IEnumerator Load()
    {
        AsyncOperation AsyncO = SceneManager.LoadSceneAsync("LAND");
        Debug.Log("AsyncO");
        bool act = true;

        while (act)
        {
            if (AsyncO.isDone)
            {
                fill.fillAmount = 1;
                act = false;
                Debug.Log("AsyncO.isDone");
                Destroy(gameObject);
            }
            else
            {
                fill.fillAmount = AsyncO.progress * 1.1f;
            }
            yield return null;
        }
    }
}
