using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog_M : MonoBehaviour
{
    public GameObject dialog_GO;
    public TextMeshProUGUI txt;

    void Start()
    {
        GL.dlg = this;
        dialog_GO.SetActive(false);

        if(Land.state_buildings["cave"].lvl <= 0) { add_txt("welcome"); }
    }

    public void add_txt(string key)
    {
        txt.text = Dialog_texts.texts[key];
        Invoke("Invoke_add_txt", 1);
    }

    void Invoke_add_txt()
    {
        dialog_GO.SetActive(true);
    }


    public void close()
    {
        dialog_GO.SetActive(false);

        if (GL.state.dialog.buy_cave == 0) 
        {
            GL.state.dialog.buy_cave = 1;
            if (Land.state_buildings["cave"].lvl <= 0) { add_txt("buy_cave"); }
        }
    }
}
