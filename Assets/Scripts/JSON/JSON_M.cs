using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON_M : MonoBehaviour
{
    static string pth;


    public static void Load()
    {
        Debug.Log("Load JSON");

        pth = Path.Combine(Application.persistentDataPath + "/data.json");

        GL.state = new GAME_STATE();


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

    public static void Save()
    {
        string json = JsonUtility.ToJson(GL.state, true);
        Debug.Log(json);

        File.WriteAllText(pth, json);
    }
}