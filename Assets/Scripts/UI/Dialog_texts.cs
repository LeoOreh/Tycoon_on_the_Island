using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dialog_texts : MonoBehaviour
{
    public static Dictionary<string, string> texts = new Dictionary<string, string>();  

    public static void Load()
    {
        TEXTS txts = new TEXTS();
        string json = Resources.Load<TextAsset>("Dialog/LNG_RU").text;
        Debug.Log(json);
        txts = JsonUtility.FromJson<TEXTS>(json);

        texts["welcome"]        = txts.welcome;
        texts["buy_cave"]       = txts.buy_cave;
        texts["buy_cave_up"]    = txts.buy_cave_up;
        texts["buy_energy"]     = txts.buy_energy;
        texts["buy_energy_up"]  = txts.buy_energy_up;
        texts["last"]           = txts.last;
    }

    [System.Serializable]
    public class TEXTS 
    { 
        public string welcome;
        public string buy_cave;
        public string buy_cave_up;
        public string buy_energy;
        public string buy_energy_up;
        public string last;
    }
}
