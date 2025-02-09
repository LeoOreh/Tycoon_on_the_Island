using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON_M : MonoBehaviour
{
    static string pth = Path.Combine(Application.persistentDataPath + "/data.json");

    public static void Load()
    {
        Debug.Log("Load JSON");


        if (File.Exists(pth))
        {
            string json = File.ReadAllText(pth);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, GL.state);
            Debug.Log("Json loading successful");
        }
        else
        {
            Debug.Log("(data JSON) file not found: " + pth);
            Save();
        }
    }

    static float TS_save;
    public static void Save()
    {
        if (Time.time > TS_save + 2)
        {
            TS_save = Time.time;

            string json = JsonUtility.ToJson(GL.state, true);
            //Debug.Log(json);

            File.WriteAllText(pth, json);
        }
    }
}