using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settings_window;
    public Slider slider;
    public static float slider_quality = 0.5f;

    void Start()
    {
        slider_quality = GL.state.slider_quality;
        slider.value = slider_quality;

        Quality();
    }

    public void Open()
    {
        settings_window.SetActive(true);
    }

    public void Close()
    {
        settings_window.SetActive(false);

        slider_quality = slider.value;
        Debug.Log(slider_quality);

        GL.state.slider_quality = slider_quality;

        Quality();

        JSON_M.Save();
    }

    void Quality()
    {
        if(slider_quality < 0.10) { QualitySettings.globalTextureMipmapLimit = 3; }else
        if(slider_quality < 0.35) { QualitySettings.globalTextureMipmapLimit = 2; }else
        if(slider_quality < 0.70) { QualitySettings.globalTextureMipmapLimit = 1; }else
                                  { QualitySettings.globalTextureMipmapLimit = 0; }
    }
}
